"use client";
import DataTable from "@/shared/table";
import { Box, Button, Flex, Group, Input, Modal, Text } from "@mantine/core";
import { IconEdit, IconSearch } from "@tabler/icons-react";
import { useMemo } from "react";
import { Column } from "react-table";
import { Data } from "./page";
import Link from "next/link";

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
        accessor: "createdAt",
      },
      {
        Header: "Action",
        Cell: ({ data }) => (
          <div className="flex space-x-4">
            <Link
              href={`/subordinate/complaint-log/${data.id}`}
              className="text-gray-500 ml-4 hover:text-gray-700"
            >
              <IconEdit className="w-5 h-5" />
            </Link>
          </div>
        ),
      },
    ],
    []
  );

  return (
    <>
      <Box className="w-full bg-primarykey-body">
        <Box className="px-2 py-5">
          <Text className="text-xl">Complaints Log</Text>
        </Box>

        <DataTable columns={columns} data={data} pageSize={5} />
      </Box>
    </>
  );
};

export default RecentComplaints;
