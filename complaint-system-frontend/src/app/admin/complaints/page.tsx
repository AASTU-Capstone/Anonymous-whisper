"use client";
import { Box } from "@mantine/core";
import Complaints from "./table";
import { useGetRecievedComplaintsForAdminQuery } from "@/lib/redux/features/admin";
import { useState, useEffect } from "react";

export interface Data {
  id: string;
  title: string;
  status: string;
  category: string;
  createdAt: string;
}

const Page = () => {
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);

  const { data: res, isLoading, isSuccess, refetch } = useGetRecievedComplaintsForAdminQuery({
    pageNumber,
    pageSize
  });

  useEffect(() => {
    refetch();
  }, [pageNumber, pageSize, refetch]);

  const data = res?.data.map((item: any) => ({ ...item, status: "received" })) || [];
  const totalCount = res?.totalCount || 0;
  console.log(pageSize)
  console.log(totalCount)
  return (
    <Box className="w-full bg-primary-background">
      <Complaints
        data={data}
        totalCount={totalCount}
        pageSize={pageSize}
        currentPage={pageNumber}
        setPageSize={setPageSize}
        setPageNumber={setPageNumber}
        refetchComplaints={refetch}
      />
    </Box>
  );
};

export default Page;
