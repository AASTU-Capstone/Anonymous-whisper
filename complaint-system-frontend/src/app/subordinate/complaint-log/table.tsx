"use client";
import DataTable from "@/shared/table";
import ViewComplaint from "@/shared/view-complaint";
import { ActionIcon, Box, Button, Flex, Input, Menu, Modal, Text } from "@mantine/core";
import { modals } from "@mantine/modals";
import {
  IconAdjustmentsHorizontal,
  IconChevronDown,
  IconEdit,
  IconEye,
  IconSearch,
  IconSquareCheck,
} from "@tabler/icons-react";
import { useMemo, useState } from "react";
import { Column } from "react-table";
import { Data } from "./page";
import Link from "next/link";
import {
  useUpdateComplaintLogStatusForSubordinateMutation,
  useGetComplaintLogByIdForSubordinateQuery
} from "@/lib/redux/features/subordinate"
import { UpdateComplaintLogStatusForSubordinate } from "@/types";
import { useDisclosure } from "@mantine/hooks";
import ViewComplaintLogById from "./viewmodal";

const getcomplaintLogById = (id:string)=>{
  const {data:complaintLogById, isLoading:complaintLogByIdLoading,isSuccess} = useGetComplaintLogByIdForSubordinateQuery(id);
  return complaintLogById;
}
 
const RecentComplaints = ({ data, refetchComplaintLogs }: { data: Data[], refetchComplaintLogs: () => void; }) => {
  const [isViewModalOpened, { open: openViewModal, close: closeViewModal }] =
    useDisclosure(false);
  const [id, setId] = useState("")
  const [complaintLog, setComplaintLog] = useState();
  console.log(data);
  const [updateComplaintLogStatus,isLoading] = useUpdateComplaintLogStatusForSubordinateMutation({})

  const handleAccept = async (id: string) => {
    modals.openConfirmModal({
      title: "Submit Complaint Log",
      centered: true,
      children: (
        <Text size="sm">Are you sure you want to submit this complaint log?</Text>
      ),
      labels: { confirm: "Submit", cancel: "Cancel" },
      confirmProps: { color: "green" },
      closeOnConfirm: true,
      onConfirm: async () => {
        // delete from the db
        const complaintLogStatus : UpdateComplaintLogStatusForSubordinate = {
          complainLogId:id,
          status:"progressing"
        };
        await updateComplaintLogStatus(complaintLogStatus)
        refetchComplaintLogs()
        return;
      },
    });
  };

  
  const handleView = async (id: string) => {
    const complaintLogById = getcomplaintLogById(id);
    setComplaintLog(complaintLogById?.data?.complaints)
    // fetch the complaint using the id
    // set to setComplaint after fetching the complaint
    // the open the modal by calling open()
    openViewModal();
  };
  const columns: Array<Column<Data>> = [
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
            value.toLocaleLowerCase() === "high"
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
      {
        Header: "Action",
        accessor:'id',
        Cell: ({ value }) => {
          console.log(value)
          
          return (
          <div className="flex space-x-4">
            <ActionIcon variant="light"
            onClick={()=>{
              setId(value)
            }}
              className="text-gray-500 hover:text-gray-700"
            >
              <IconEye className="w-5 h-5" />
            </ActionIcon>
            <ActionIcon variant="light">
            <Link
              href={`/subordinate/complaint-log/${value}`}
              className="text-gray-500 hover:text-gray-700"
            >
              <IconEdit className="w-5 h-5" />
            </Link>
            </ActionIcon>
            <ActionIcon variant="light" onClick={() => handleAccept(value)}>
            
              <IconSquareCheck color="green" className="w-5 h-5" />
            </ActionIcon>
          </div>
        )}
      },
    ]

  return (
    <>
      <Text className="text-primary-default font-bold text-2xl mb-5">
        Complaints
      </Text>
      <Flex className="gap-3 mb-5 items-center">
        <Input
          placeholder="Search"
          radius="md"
          w={350}
          leftSection={<IconSearch />}
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

      <Box className="w-full bg-primary-body">
        <Box className="px-2 py-5">
          <Text className="text-xl">Complaint Logs</Text>
        </Box>

        <DataTable columns={columns} data={data} pageSize={5} />
        <ViewComplaintLogById id = {id} openViewModal={openViewModal} closeViewModal= {closeViewModal} isViewModalOpened = {isViewModalOpened}/>
      </Box>
    </>
  );
};

export default RecentComplaints;
