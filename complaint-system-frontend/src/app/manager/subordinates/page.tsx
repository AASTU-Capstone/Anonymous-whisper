import { Box } from "@mantine/core";
import SubordinatesTable from "./table";
import { useGetSubordinatesQuery } from "@/lib/redux/features/manager";
import { GetSubordinatesResponse } from "@/types";

const page = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetSubordinatesQuery({});

  const data =
    res?.data?.map((item: GetSubordinatesResponse) => {
      return {
        ...item,
      };
    }) || [];

  return (
    <>
      <Box className="w-full bg-primary-background">
        <SubordinatesTable data={data} refetchSubordinate={refetch} />
      </Box>
    </>
  );
};

export default page;
