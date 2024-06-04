"use client";
import { Box } from "@mantine/core";
import ManagersList from "./table";
import { useGetManagersForAdminQuery } from "@/lib/redux/features/admin";

export interface Data {
  id: string;
  name: string;
  role: string;
  email: string;
  createdAt: string;
}

const page = () => {
  const { data: res, isLoading, isSuccess } = useGetManagersForAdminQuery({});
  const type1 = res?.data?.type1;
  const type2 = res?.data?.type2;
  const data = [type1, type2];
  return (
    <>
      <Box className="w-full bg-primary-background">
        <ManagersList data={data} />
      </Box>
    </>
  );
};

export default page;
