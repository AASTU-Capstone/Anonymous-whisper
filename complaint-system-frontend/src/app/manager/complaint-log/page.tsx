"use client";
import { Box } from "@mantine/core";
import ComplaintsLogBody from "./table";
import { useGetComplaintLogToUpdateForManagerQuery } from "@/lib/redux/features/manager";
import { GetComplaintLogToUpdateForManagerResponse } from "@/types";
import { useState, useEffect } from "react";

const ComplaintsLog = () => {
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);

  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetComplaintLogToUpdateForManagerQuery({
    pageNumber,
    pageSize
  });

  useEffect(() => {
    refetch();
  }, [pageNumber, pageSize, refetch]);

  const data =
    res?.data?.map((item: GetComplaintLogToUpdateForManagerResponse) => {
      return {
        ...item,
      };
    }) || [];

  const totalCount = res?.totalCount || 0;

  return (
    <Box className="w-full bg-primary-background">
      <ComplaintsLogBody
        data={data}
        totalCount={totalCount}
        pageSize={pageSize}
        currentPage={pageNumber}
        setPageSize={setPageSize}
        setPageNumber={setPageNumber}
        refetchComplaintLogs={refetch}
      />
    </Box>
  );
};

export default ComplaintsLog;
