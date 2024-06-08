"use client";
import React, { useState, useEffect } from "react";
import { useGetComplaintsQuery } from "@/lib/redux/features/user";
import MyComplaints from "./table";
import { Box, Text } from "@mantine/core";

const Page: React.FC = () => {
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);

  const { data: res, isLoading, isSuccess, refetch } = useGetComplaintsQuery({
    pageNumber,
    pageSize,
  });

  useEffect(() => {
    refetch();
  }, [pageNumber, pageSize, refetch]);

  const data = res?.data || [];
  const totalCount = res?.totalCount || 0;

  return (
    <Box className="w-full bg-primarykey-body">
      <Box className="px-2 py-5">
        <Text className="text-xl font-bold">My Complaints</Text>
      </Box>

      {isSuccess && (
        <MyComplaints
          data={data}
          totalCount={totalCount}
          pageSize={pageSize}
          currentPage={pageNumber}
          setPageSize={setPageSize}
          setPageNumber={setPageNumber}
        />
      )}
    </Box>
  );
};

export default Page;
