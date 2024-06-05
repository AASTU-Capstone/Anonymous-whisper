"use client";
import { Box } from "@mantine/core";
import ManagersList from "./table";
import { useGetManagersForAdminQuery } from "@/lib/redux/features/admin";
import {ManagerResponse} from "@/types";

const page = () => {
  const { data: res, isLoading, isSuccess, refetch } = useGetManagersForAdminQuery({});
  const type1 = res?.data?.type1;
  const type2 = res?.data?.type2;
  const added = [type1, type2];

  const data =
    added.map((item: ManagerResponse) => {
      return {
        ...item,
      };
    }) || [];

  return (
    <>
      <Box className="w-full bg-primary-background">
        <ManagersList data={data} refetchManagers={refetch} />
      </Box>
    </>
  );
};

export default page;
