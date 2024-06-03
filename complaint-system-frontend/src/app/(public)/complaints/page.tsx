"use client";
import React from "react";
import { GetComplaintsForUserResponse } from "@/types";
import { useGetComplaintsQuery } from "@/lib/redux/features/user";
import MyComplaints from "./table";
import { Box, Text } from "@mantine/core";

const Page: React.FC = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetComplaintsQuery({});

  const data =
    res?.data?.map((item: GetComplaintsForUserResponse) => {
      return {
        ...item,
      };
    }) || [];

  return (
    <>
      <Box className="w-full bg-primarykey-body">
        <Box className="px-2 py-5">
          <Text className="text-xl font-bold">My Complaints</Text>
        </Box>

        <MyComplaints data={data} />
      </Box>
    </>
  );
};

export default Page;
