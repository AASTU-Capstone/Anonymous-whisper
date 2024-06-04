import { Box } from "@mantine/core";
import AssignSubordinateTable from "./table";
import { useGetComplaintLogToAssignForManagerQuery } from "@/lib/redux/features/manager";
import { useMemo, useState } from "react";
import { GetComplaintLogToAssignForManagerResponse } from "@/types";

// export interface Data {
//   id: string;
//   title: string;
//   priority: string;
//   manager: string;
//   createdDate: string;
// }

// const data: Data[] = [
//   {
//     id: "1",
//     title: "David Wagner",
//     priority: "high",
//     createdDate: "24 Oct, 2015",
//     manager: "Lorem Ipsum",
//   },
//   {
//     id: "2",
//     title: "Ina Hogan",
//     priority: "medium",
//     createdDate: "24 Oct, 2015",
//     manager: "Lorem Ipsum",
//   },
//   {
//     id: "3",
//     title: "Devin Harmon",
//     priority: "low",
//     createdDate: "18 Dec, 2015",
//     manager: "Lorem Ipsum",
//   },
//   {
//     id: "4",
//     title: "Lena Page",
//     priority: "medium",
//     createdDate: "8 Oct, 2016",
//     manager: "Lorem Ipsum",
//   },
//   {
//     id: "5",
//     title: "Eula Horton",
//     priority: "high",
//     createdDate: "15 Jun, 2017",
//     manager: "Lorem Ipsum",
//   },
//   {
//     id: "6",
//     title: "Victoria Perez",
//     priority: "high",
//     createdDate: "12 Jan, 2019",
//     manager: "Lorem Ipsum",
//   },
//   {
//     id: "7",
//     title: "Cora Medina",
//     priority: "low",
//     createdDate: "21 July, 2020",
//     manager: "Lorem Ipsum",
//   },
// ];
const {
  data: res,
  isLoading,
  isSuccess,
  refetch,
} = useGetComplaintLogToAssignForManagerQuery({});

const [searchQuery, setSearchQuery] = useState("");

const data =
  res?.data?.map((item: GetComplaintLogToAssignForManagerResponse) => {
    return {
      ...item,
    };
  }) || [];

const filteredData = useMemo(() => {
  return data.filter((item: GetComplaintLogToAssignForManagerResponse) =>
    item.title.toLowerCase().includes(searchQuery.toLowerCase())
  );
}, [searchQuery, data]);

const AssignSubordinate = () => {
  return (
    <Box className="w-full bg-primary-background">
      <AssignSubordinateTable
        data={filteredData}
        refetchComplaintLog={refetch}
      />
    </Box>
  );
};

export default AssignSubordinate;
