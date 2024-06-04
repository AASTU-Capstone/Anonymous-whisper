"use client";
import { Box } from "@mantine/core";
import ComplaintsLogBody from "./table";
import { useGetComplaintLogsToUpdateForAdminQuery } from "@/lib/redux/features/admin";

export interface Data {
  id: string;
  title: string;
  priority: string;
  createdAt: string;
}

const ComplaintsLog = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetComplaintLogsToUpdateForAdminQuery({});
  console.log(res);
  const data =
    res?.data?.map((item: any) => {
      return {
        ...item,
      };
    }) || [];
  return (
    <Box className="w-full bg-primary-background">
      <ComplaintsLogBody data={data} refetchComplaintLogs={refetch} />
    </Box>
  );
};

export default ComplaintsLog;
