"use client";
import { Box } from "@mantine/core";
import RecentComplaints from "./table";
import { useGetComplaintLogsToUpdateForSubordinateQuery } from "@/lib/redux/features/subordinate";
import { useState } from "react";
import { useDisclosure } from "@mantine/hooks";

export interface Data {
  id: string;
  title: string;
  priority: string;
  manager: string;
  createdAt: string;
}

const ComplaintsLog = () => {
  const {data:res,isLoading,isSuccess,refetch} = useGetComplaintLogsToUpdateForSubordinateQuery({})
  const [id, setId] = useState("")
  const [isViewModalOpened, { open: openViewModal, close: closeViewModal }] =
    useDisclosure(false);
  const data = res?.data?.map((item:any)=>{
    return {
      ...item,
    }
  }) || []


  return (
    <Box className="w-full bg-primary-background">
      <RecentComplaints data={data} refetchComplaintLogs={refetch} />
    </Box>
  );
};

export default ComplaintsLog;
