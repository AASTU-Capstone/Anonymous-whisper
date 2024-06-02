"use client";
import React, { useMemo } from "react";
import DataTable from "@/shared/table";
import { IconEdit, IconTrash } from "@tabler/icons-react";
import { Column } from "react-table";
import { Box, Button, Flex, Group, Modal, Select, Text, TextInput } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";

interface Data {
  id: string;
  title: string;
  status: string;
  createdDate: string;
  category: string;
}

const Page: React.FC = () => {
  const [opened, { open, close }] = useDisclosure(false);

  const handleEdit = (id: string) => {
    open();
  };
  const handleDelete = (id: string) => {
    console.log(`Delete item with id: ${id}`);
  };

  const columns: Array<Column<Data>> = useMemo(
    () => [
      {
        Header: "Complaints Title",
        accessor: "title",
        Cell: ({ value }) => (
          <div className="text-sm font-medium text-gray-900">{value}</div>
        ),
      },
      {
        Header: "Status",
        accessor: "status",
        Cell: ({ value }) => {
          const statusClass =
            value === "Resolved"
              ? "bg-green-200 text-green-800"
              : value === "Inprogress"
                ? "bg-blue-200 text-blue-800"
                : value === "Rejected"
                  ? "bg-red-200 text-red-800"
                  : "bg-gray-200 text-gray-800";
          return (
            <span
              className={`px-3 py-1 inline-flex text-xs leading-5 font-semibold rounded-full ${statusClass}`}
            >
              {value}
            </span>
          );
        },
      },
      {
        Header: "Created Date",
        accessor: "createdDate",
      },
      {
        Header: "Category",
        accessor: "category",
      },
      {
        Header: "Action",
        Cell: ({ row }) => (
          <div className="flex space-x-4">
            <button
              onClick={() => handleEdit(row.original.id)}
              className="text-gray-500 hover:text-gray-700"
            >
              <IconEdit className="w-5 h-5" />
            </button>
            <button
              onClick={() => handleDelete(row.original.id)}
              className="text-gray-500 hover:text-gray-700"
            >
              <IconTrash className="w-5 h-5" />
            </button>
          </div>
        ),
      },
    ],
    []
  );

  const data: Data[] = useMemo(
    () => [
      {
        id: "1",
        title: "David Wagner",
        status: "Resolved",
        createdDate: "24 Oct, 2015",
        category: "Lorem Ipsum",
      },
      {
        id: "2",
        title: "Ina Hogan",
        status: "Inprogress",
        createdDate: "24 Oct, 2015",
        category: "Lorem Ipsum",
      },
      {
        id: "3",
        title: "Devin Harmon",
        status: "Rejected",
        createdDate: "18 Dec, 2015",
        category: "Lorem Ipsum",
      },
      {
        id: "4",
        title: "Lena Page",
        status: "Received",
        createdDate: "8 Oct, 2016",
        category: "Lorem Ipsum",
      },
      {
        id: "5",
        title: "Eula Horton",
        status: "Inprogress",
        createdDate: "15 Jun, 2017",
        category: "Lorem Ipsum",
      },
      {
        id: "6",
        title: "Victoria Perez",
        status: "Inprogress",
        createdDate: "12 Jan, 2019",
        category: "Lorem Ipsum",
      },
      {
        id: "7",
        title: "Cora Medina",
        status: "Received",
        createdDate: "21 July, 2020",
        category: "Lorem Ipsum",
      },
    ],
    []
  );

  return (<>
   <Modal centered opened={opened} onClose={close} title="Edit Complaint">
        <Flex className="flex-col my-5 gap-7">
          <TextInput placeholder="title" />
          <Select placeholder="Priority" data={["High", "Medium", "Low"]} />
          <Select
            placeholder="Select Manager"
            data={["manager 1", "manager 2", "manager 3"]}
          />
        </Flex>
        <Group justify="end">
          <Button>Update</Button>
          <Button variant="light" onClick={close}>
            Cancel
          </Button>
        </Group>
      </Modal>
    <Box className="w-full bg-primarykey-body">
      <Box className="px-2 py-5">
        <Text className="text-xl font-bold">My Complaints</Text>
      </Box>

      <DataTable columns={columns} data={data} pageSize={10} />
    </Box>
  </>
  );
};

export default Page;
