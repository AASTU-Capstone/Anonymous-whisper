"use client";
import { Box } from "@mantine/core";
import Resources from "./table"
import { useGetAllResourcesQuery } from "@/lib/redux/features/resource";

export interface Data {
  id: string;
  title: string;
  createdAt: string;
}
const ResourceData = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetAllResourcesQuery({});
  console.log(res);
  const data =
    res?.data?.map((item: any) => {
      return {
        ...item,
      };
    }) || [];
  return (
    <Box className="w-full bg-primary-background">
      <Resources data={data}/>
    </Box>
  );
};

export default ResourceData;