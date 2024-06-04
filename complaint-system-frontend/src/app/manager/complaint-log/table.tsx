"use client";
import DataTable from "@/shared/table";
import ViewComplaintResponse from "@/shared/view-complaint-reponse";
import { Box, Button, Flex, Input, Menu, Modal, Text } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import { modals } from "@mantine/modals";
import {
  IconAdjustmentsHorizontal,
  IconChevronDown,
  IconEye,
  IconSearch,
  IconSquareCheck,
  IconSquareX,
} from "@tabler/icons-react";
import { useMemo, useState } from "react";
import { Column } from "react-table";
import {
  GetComplaintLogToUpdateForManagerResponse,
  UpdateComplaintLogStatusInput,
} from "@/types/";
import { useUpdateComplaintLogStatusMutation } from "@/lib/redux/features/manager";

const ComplaintsLogBody = ({
  data,
  refetchComplaintLogs,
}: {
  data: GetComplaintLogToUpdateForManagerResponse[];
  refetchComplaintLogs: () => void;
}) => {
  const [isViewModalOpened, { open: openViewModal, close: closeViewModal }] =
    useDisclosure(false);
  const [updateComplaintLogStatus] = useUpdateComplaintLogStatusMutation();
  console.log(data)
  const [complaint, setComplaint] = useState();
  const [rejecting, isRejecting] = useState(false);

  const [searchQuery, setSearchQuery] = useState("");

  const filteredData = useMemo(() => {
    return data.filter((item: GetComplaintLogToUpdateForManagerResponse) =>
      item.title.toLowerCase().includes(searchQuery.toLowerCase())
    );
  }, [searchQuery, data]);

  const handleAccept = (id: string) => {
    modals.openConfirmModal({
      title: "Accept Complaint",
      centered: true,
      children: (
        <Text size="sm">Are you sure you want to Accept this complaint?</Text>
      ),
      labels: { confirm: "Accept Complaint", cancel: "Cancel" },
      confirmProps: { color: "green" },
      closeOnConfirm: true,
      onConfirm: async () => {
        const input: UpdateComplaintLogStatusInput = {
          complainLogId: id,
          status: "submitted",
        };
        await updateComplaintLogStatus(input).unwrap();
        refetchComplaintLogs();
        return;
      },
    });
  };

  const handleReject = (id: string) => {
    modals.openConfirmModal({
      title: "Reject Complaint",
      centered: true,
      children: (
        <Text size="sm">Are you sure you want to Reject this complaint?</Text>
      ),
      labels: { confirm: "Reject Complaint", cancel: "Cancel" },
      confirmProps: { color: "red" },
      closeOnConfirm: true,
      onConfirm: async () => {
        const input: UpdateComplaintLogStatusInput = {
          complainLogId: id,
          status: "processing",
        };
        console.log(input)
        await updateComplaintLogStatus(input)
        refetchComplaintLogs();
        return;
      },
    });
  };

  const handleView = (id: string) => {
    // fetch the complaint using the id
    // set to setComplaint after fetching the complaint
    // the open the modal by calling open()
    openViewModal();
  };

  const columns: Array<Column<GetComplaintLogToUpdateForManagerResponse>> =
    useMemo(
      () => [
        {
          Header: "Title",
          accessor: "title",
          Cell: ({ value }) => (
            <div className="text-sm font-medium text-gray-900">{value}</div>
          ),
        },
        {
          Header: "Priority",
          accessor: "priority",
          Cell: ({ value }) => {
            const statusClass =
              value === "high"
                ? "bg-red-200 text-red-800"
                : value === "medium"
                  ? "bg-blue-200 text-blue-800"
                  : "bg-gray-200 text-gray-800";
            return (
              <span
                className={`py-1 px-5 text-center text-xs leading-5 font-semibold rounded-full ${statusClass}`}
              >
                {value}
              </span>
            );
          },
        },
        {
          Header: "Created Date",
          accessor: "createdAt",
        },
        // {
        //   Header: "Subordinate",
        //   accessor: "subordinate",
        // },
        {
          Header: "Action",
          accessor: 'id',
          Cell: ({ value }) => (
            <div className="flex space-x-4">
              <button
                onClick={() => handleView(value)}
                className="text-gray-500 hover:text-gray-700"
              >
                <IconEye className="w-5 h-5" />
              </button>
              <button
                onClick={() => handleAccept(value)}
                className="text-gray-500 hover:text-gray-700"
              >
                <IconSquareCheck color="green" className="w-5 h-5" />
              </button>
              <button
                onClick={() => handleReject(value)}
                className="text-gray-500 hover:text-gray-700"
              >
                <IconSquareX color="red" className="w-5 h-5" />
              </button>
            </div>
          ),
        },
      ],
      []
    );

  return (
    <>
      <Modal
        size="70%"
        centered
        opened={isViewModalOpened}
        onClose={closeViewModal}
        title="Complaint"
      >
        <ViewComplaintResponse complaint={complaint} />
      </Modal>
      <Text className="text-primary-default  font-bold text-2xl mb-5">
        Complaints
      </Text>
      <Flex className="gap-3 items-center">
        <Input
          placeholder="Search"
          radius="md"
          w={350}
          leftSection={<IconSearch />}
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
        />

        <Button>Search</Button>

        <Menu>
          <Menu.Target>
            <Button
              variant="transparent"
              className="text-primary-text"
              rightSection={<IconChevronDown />}
            >
              Sort by
            </Button>
          </Menu.Target>

          <Menu.Dropdown>
            <Menu.Item>Items</Menu.Item>
          </Menu.Dropdown>
        </Menu>
        <Menu>
          <Menu.Target>
            <Button
              variant="transparent"
              className="text-primary-text"
              rightSection={<IconChevronDown />}
            >
              Saved Search
            </Button>
          </Menu.Target>

          <Menu.Dropdown>
            <Menu.Item>Items</Menu.Item>
          </Menu.Dropdown>
        </Menu>
        <IconAdjustmentsHorizontal className="cursor-pointer" />
      </Flex>
      <Box className="w-full mt-7">
        <Box>
          <Text className="text-xl px-5 py-4 bg-primary-body">
            My Complaints
          </Text>
        </Box>

        <DataTable columns={columns} data={filteredData} pageSize={5} />
      </Box>
    </>
  );
};

export default ComplaintsLogBody;
