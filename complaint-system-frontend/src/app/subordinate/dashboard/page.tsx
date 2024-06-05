"use client";
import { Box, Paper, SimpleGrid, Text } from "@mantine/core";
import RecentComplaints from "./table";
import { useGetComplaintLogStatisticsQuery,useGetCorruptionTrendStatisticsQuery } from "@/lib/redux/features/statistics";
import { useGetAllComplaintsForAdminQuery } from "@/lib/redux/features/admin";
import jwt from "jsonwebtoken"
export interface Data {
  id: string;
  title: string;
  category: string;
  status: string;
  tags: string;
  createdAt: string;
}

const Dashboard = () => {
  const token = decodeURIComponent(typeof window !== "undefined" ? document.cookie : "")
    .split(";")
    .find((c) => c.trim().startsWith("token="))
    ?.split("=")[1];
  const decodedToken: any = jwt.decode(token || "");
  const subordinateId = decodedToken?.userid
  const {
    data: res,
    isLoading,
    isSuccess,
  } = useGetComplaintLogStatisticsQuery({subordinateId:subordinateId, managerId:""});
  const complaintData = res?.data;
  
  return (
    <Box className="py-6 w-full bg-primarykey-background">
      <SimpleGrid
        className="w-full"
        cols={{ base: 1, sm: 2, lg: 4 }}
        spacing={{ base: 8, sm: "lg" }}
        verticalSpacing={{ base: "md", sm: "xl" }}
      >
        <Paper className="py-4 px-7">
          <Text c="dimmed">Total Complaint Logs</Text>
          <Text className="font-bold mt-1 text-xl">
            {complaintData?.totalComplaintLogs || 0}
          </Text>
        </Paper>
        <Paper className="py-4 px-7">
          <Text c="dimmed">Total Resolved</Text>
          <Text className="font-bold mt-1 text-xl">
            {complaintData?.resolvedComplaintLogs || 0}
          </Text>
        </Paper>
        <Paper className="py-4 px-7">
          <Text c="dimmed">In-Review Complaints</Text>
          <Text className="font-bold mt-1 text-xl">
            {complaintData?.pendingComplaintLogs || 0}
          </Text>
        </Paper>
        
      </SimpleGrid>

      <Box className="h-52 w-full flex justify-center items-center mt-6 bg-gray-200">
        <h1 className="text-2xl">Some Analytic Data</h1>
      </Box>
    </Box>
  );
};

export default Dashboard;
