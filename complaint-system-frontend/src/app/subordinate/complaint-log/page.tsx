"use client";
import { Box } from "@mantine/core";
import RecentComplaints from "./table";
import { useGetComplaintLogsToUpdateForSubordinateQuery } from "@/lib/redux/features/subordinate";
import { useState, useEffect } from "react";
import { useDisclosure } from "@mantine/hooks";

export interface Data {
  id: string;
  title: string;
  priority: string;
  manager: string;
  createdAt: string;
}

const ComplaintsLog = () => {
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetComplaintLogsToUpdateForSubordinateQuery({
    pageNumber,
    pageSize
  });

  useEffect(() => {
    refetch();
  }, [pageNumber, pageSize, refetch]);

  const data =
    res?.data?.map((item: any) => {
      return {
        ...item,
      };
    }) || [];

  const totalCount = res?.totalCount || 0;

  return (
    <Box className="w-full bg-primary-background">
      <RecentComplaints
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
