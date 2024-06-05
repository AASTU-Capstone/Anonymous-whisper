"use client";
import { Box } from "@mantine/core";
import ComplaintsLogBody from "./table";
import { useGetComplaintLogToUpdateForManagerQuery } from "@/lib/redux/features/manager";
import { GetComplaintLogToUpdateForManagerResponse } from "@/types";
import { useState, useMemo } from "react";

const ComplaintsLog = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetComplaintLogToUpdateForManagerQuery({});

  const data =
    res?.data?.map((item: GetComplaintLogToUpdateForManagerResponse) => {
      return {
        ...item,
      };
    }) || [];
  console.log(data)
  return (
    <Box className="w-full bg-primary-background">
      <ComplaintsLogBody data={data} refetchComplaintLogs={refetch} />
    </Box>
  );
};

export default ComplaintsLog;
