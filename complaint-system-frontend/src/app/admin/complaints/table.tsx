"use client";
import React, { useMemo } from "react";
import DataTable from "@/shared/table";
import { IconEdit, IconTrash } from "@tabler/icons-react";
import { Column } from "react-table";
import { Box, Text } from "@mantine/core";
import { Data } from "./page";

const Complaints = ({ data }: { data: Data[] }) => {
  const handleEdit = (id: string) => {
    console.log(`Edit item with id: ${id}`);
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
        Header: "Issuer",
        accessor: "issuer",
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

  return (
    <Box className="w-full bg-primarykey-body">
      <Box className="px-2 py-5">
        <Text className="text-xl">Complaints</Text>
      </Box>

      <DataTable columns={columns} data={data} pageSize={10} />
    </Box>
  );
};

export default Complaints;
