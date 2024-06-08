"use client";
import { IconEye } from "@tabler/icons-react";
import { Data } from "./page";
import Link from "next/link";
import { Column } from "react-table";
import DataTable from "@/shared/table";
import { ActionIcon, Box, Button, Flex, Input, Menu, Modal, Text } from "@mantine/core";
const Resources = ({data}:{data: Data[]})=>{



    const columns: Array<Column<Data>> = [
        {
          Header: "Title",
          accessor: "title",
          Cell: ({ value }) => (
            <div className="text-sm font-medium text-gray-900">{value}</div>
          ),
        },
        {
          Header: "Action",
          accessor:'id',
          Cell: ({ value }) => {
            console.log(value)
            
            return (
            <div className="flex space-x-4">
              <ActionIcon variant="light">
               <Link
               href={`/resources/${value}`}
               className="text-gray-500 hover:text-gray-700"
             >
               <IconEye className="w-5 h-5" />
             </Link>
              </ActionIcon>
            </div>
          )}
        },
      ]
    return (

        <>
        <Text className="text-primary-default font-bold text-2xl mb-5">
            Resources
        </Text>
        <DataTable columns={columns} data={data} pageSize={5} />
        </>
    );
};

export default Resources;