"use client";
import DataTable from "@/shared/table";
import { Box, Flex, Paper, SimpleGrid, Text } from "@mantine/core";
import { IconCheck, IconTrash } from "@tabler/icons-react";
import { useMemo } from "react";
import { Column } from "react-table";
import { Data } from "./page";

const RecentComplaints = ({ data }: { data: Data[] }) => {
  const columns: Array<Column<Data>> = useMemo(
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
        accessor: "createdDate",
      },
      {
        Header: "Manager",
        accessor: "manager",
      },
    ],
    []
  );

  return (
    <Box className="w-full mt-7">
      <Box>
        <Text className="text-xl px-5 py-4 bg-primarykey-body">
          Complaints Log
        </Text>
      </Box>

      <DataTable columns={columns} data={data} pageSize={5} />
    </Box>
  );
};

export default RecentComplaints;
