"use client";
import DataTable from "@/shared/table";
import { Box, Text } from "@mantine/core";
import { useMemo, useState } from "react";
import { Column } from "react-table";
import { GetComplaintsForUserResponse } from "@/types";
import { useDisclosure } from "@mantine/hooks";

const MyComplaints = ({ data }: { data: GetComplaintsForUserResponse[] }) => {
  const [opened, { open, close }] = useDisclosure(false);
  const columns: Array<Column<GetComplaintsForUserResponse>> = useMemo(
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
        accessor: "createdAt",
      },
      {
        Header: "Category",
        accessor: "category",
      },
    ],
    []
  );

  return (
    <>
      <Box className="w-full bg-primarykey-body">
        <DataTable columns={columns} data={data} pageSize={5} />
      </Box>
    </>
  );
};

export default MyComplaints;
