"use client";
import { Box } from "@mantine/core";
import ComplaintsLogBody from "./table";
import { useGetComplaintLogsToUpdateForAdminQuery } from "@/lib/redux/features/admin";
import { useState, useEffect } from "react";

export interface Data {
  id: string;
  title: string;
  priority: string;
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
  } = useGetComplaintLogsToUpdateForAdminQuery({ pageNumber, pageSize });

  useEffect(() => {
    refetch();
  }, [pageNumber, pageSize, refetch]);

  const data = res?.data || [];
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
