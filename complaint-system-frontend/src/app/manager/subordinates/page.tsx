"use client";
import { Box } from "@mantine/core";
import SubordinatesList from "./table";
import { useGetSubordinatesQuery } from "@/lib/redux/features/manager";
import { GetSubordinatesResponse } from "@/types";
import { useState, useEffect } from "react";

const Page = () => {
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);

  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetSubordinatesQuery({ pageNumber, pageSize });

  useEffect(() => {
    refetch();
  }, [pageNumber, pageSize, refetch]);

  const data =
    res?.data?.map((item: GetSubordinatesResponse) => ({
      ...item,
    })) || [];

  return (
    <Box className="w-full bg-primary-background">
      <SubordinatesList
        data={data}
        totalCount={res?.totalCount || 0}
        pageSize={pageSize}
        currentPage={pageNumber}
        setPageSize={setPageSize}
        setPageNumber={setPageNumber}
        refetchSubordinate={refetch}
      />
    </Box>
  );
};

export default Page;
