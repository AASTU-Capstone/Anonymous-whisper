"use client";
import DataTable from "@/shared/table";
import { Box, Flex, Paper, SimpleGrid, Text } from "@mantine/core";
import { IconCheck, IconTrash } from "@tabler/icons-react";
import { useMemo } from "react";
import { Column } from "react-table";
import { Data } from "./page";

const ManagersList = ({ data }: { data: Data[] }) => {
  const columns: Array<Column<Data>> = useMemo(
    () => [
      {
        Header: "Name",
        accessor: "name",
        Cell: ({ value }) => (
          <div className="text-sm font-medium text-gray-900">{value}</div>
        ),
      },
      {
        Header: "Role",
        accessor: "role",
        Cell: ({ value }) => {
          const statusClass =
            value === "Employee"
              ? "bg-gray-200 text-gray-500"
              : "bg-blue-500 text-white";
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
        Header: "Email",
        accessor: "email",
      },
    ],
    []
  );

  return (
    <Box className="w-full mt-7">
      <Box>
        <Text className="text-xl px-5 py-4 bg-primarykey-body">Managers List</Text>
      </Box>

      <DataTable columns={columns} data={data} pageSize={5} />
    </Box>
  );
};

export default ManagersList;
