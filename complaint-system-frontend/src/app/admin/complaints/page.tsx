"use client";
import { useGetRecievedComplaintsForAdminQuery } from "@/lib/redux/features/admin";
import Complaints from "./table";

export interface Data {
  id: string;
  title: string;
  status: string;
  category: string;
  createdAt: string;
}

const page = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetRecievedComplaintsForAdminQuery({});
  const data =
    res?.data.map((item: any) => {
      return {
        ...item,
      };
    }) || [];

  return <Complaints data={data} refetchComplaints={refetch} />;
};

export default page;
