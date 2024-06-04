"use client";
import React from "react";
import AssignComplaintTable from "./table";
import { useGetAcceptedComplaintsForAdminQuery } from "@/lib/redux/features/admin";

export interface Data {
  id: string;
  title: string;
  status: string;
  createdAt: string;
  category: string;
}

const page: React.FC = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
  } = useGetAcceptedComplaintsForAdminQuery({});
  const data =
    res?.data?.map((item: any) => {
      return {
        ...item,
        status: "accepted",
      };
    }) || [];
  return <AssignComplaintTable data={data} />;
};

export default page;
