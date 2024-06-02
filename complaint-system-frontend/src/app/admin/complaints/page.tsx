"use client";
import React, { useMemo } from "react";
import { Box, Text } from "@mantine/core";
import {useGetRecievedComplaintsForAdminQuery} from "@/lib/redux/features/admin"
import Complaints from "./table";

export interface Data {
  id: string;
  title: string;
  category: string;
  createdAt: string;
}
// const data: Data[] = [
//   {
//     id: "1",
//     title: "David Wagner",
//     status: "Resolved",
//     createdDate: "24 Oct, 2015",
//     issuer: "Lorem Ipsum",
//   },
//   {
//     id: "2",
//     title: "Ina Hogan",
//     status: "Inprogress",
//     createdDate: "24 Oct, 2015",
//     issuer: "Lorem Ipsum",
//   },
//   {
//     id: "3",
//     title: "Devin Harmon",
//     status: "Rejected",
//     createdDate: "18 Dec, 2015",
//     issuer: "Lorem Ipsum",
//   },
//   {
//     id: "4",
//     title: "Lena Page",
//     status: "Received",
//     createdDate: "8 Oct, 2016",
//     issuer: "Lorem Ipsum",
//   },
//   {
//     id: "5",
//     title: "Eula Horton",
//     status: "Inprogress",
//     createdDate: "15 Jun, 2017",
//     issuer: "Lorem Ipsum",
//   },
//   {
//     id: "6",
//     title: "Victoria Perez",
//     status: "Inprogress",
//     createdDate: "12 Jan, 2019",
//     issuer: "Lorem Ipsum",
//   },
//   {
//     id: "7",
//     title: "Cora Medina",
//     status: "Received",
//     createdDate: "21 July, 2020",
//     issuer: "Lorem Ipsum",
//   },
// ];

const page = () => {
  const {data:res, isLoading, isSuccess}  = useGetRecievedComplaintsForAdminQuery({})
  const data = res?.data.map((item:any)=>{
    return {
      ...item
    }
  })|| []
  
  return <Complaints data={data} />;
  
};

export default page;
