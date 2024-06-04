"use client";
import { Box, Paper, SimpleGrid, Text } from "@mantine/core";
import RecentComplaints from "./table";
import { useGetComplaintStatitisticsQuery } from "@/lib/redux/features/statistics";
import { useGetAllComplaintsForAdminQuery } from "@/lib/redux/features/admin";

export interface Data {
  id: string;
  title: string;
  category: string;
  status: string;
  tags: string;
  createdAt: string;
}

const Dashboard = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
  } = useGetComplaintStatitisticsQuery("");
  const {
    data: complaintResponse,
    isLoading: loading,
    isSuccess: success,
  } = useGetAllComplaintsForAdminQuery({});
  const complaintData = res?.data;
  const complaintList =
    complaintResponse?.data?.map((item: any) => {
      return {
        ...item,
      };
    }) || [];
  return (
    <Box className="py-6 w-full bg-primarykey-background">
      <SimpleGrid
        className="w-full"
        cols={{ base: 1, sm: 2, lg: 4 }}
        spacing={{ base: 8, sm: "lg" }}
        verticalSpacing={{ base: "md", sm: "xl" }}
      >
        <Paper className="py-4 px-7">
          <Text c="dimmed">Total Complaints</Text>
          <Text className="font-bold mt-1 text-xl">
            {complaintData?.totalComplaints || 0}
          </Text>
        </Paper>
        <Paper className="py-4 px-7">
          <Text c="dimmed">Total Resolved</Text>
          <Text className="font-bold mt-1 text-xl">
            {complaintData?.resolvedComplaints || 0}
          </Text>
        </Paper>
        <Paper className="py-4 px-7">
          <Text c="dimmed">In-Review Complaints</Text>
          <Text className="font-bold mt-1 text-xl">
            {complaintData?.pendingComplaints || 0}
          </Text>
        </Paper>
        <Paper className="py-4 px-7">
          <Text c="dimmed">Rejected Complaints</Text>
          <Text className="font-bold mt-1 text-xl">
            {complaintData?.rejectedComplaints || 0}
          </Text>
        </Paper>
      </SimpleGrid>

      <Box className="h-52 w-full flex justify-center items-center mt-6 bg-gray-200">
        <h1 className="text-2xl">Some Analytic Data</h1>
      </Box>
      {<RecentComplaints data={complaintList} />}
    </Box>
  );
};

export default Dashboard;
