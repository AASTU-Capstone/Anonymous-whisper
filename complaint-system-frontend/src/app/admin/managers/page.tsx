"use client";
import { Box, Button, Flex, Input, Menu, Text } from "@mantine/core";
import RecentComplaints from "./table";
import { useMemo } from "react";
import {useGetManagersForAdminQuery} from "@/lib/redux/features/admin"
import {
  IconAdjustmentsHorizontal,
  IconChevronDown,
  IconSearch,
} from "@tabler/icons-react";
import { list } from "postcss";

export interface Data {
  id: string;
  name: string;
  role: string;
  email: string;
  createdAt: string;
}

// const data: Data[] = [
//   {
//     id: "1",
//     name: "David Wagner",
//     role: "Super Admin",
//     createdDate: "24 Oct, 2015",
//     email: "Lorem Ipsum",
//   },
//   {
//     id: "2",
//     name: "Ina Hogan",
//     role: "Admin",
//     createdDate: "24 Oct, 2015",
//     email: "Lorem Ipsum",
//   },
//   {
//     id: "3",
//     name: "Devin Harmon",
//     role: "Super Admin",
//     createdDate: "18 Dec, 2015",
//     email: "Lorem Ipsum",
//   },
//   {
//     id: "4",
//     name: "Lee Harmon",
//     role: "Employee",
//     createdDate: "18 Dec, 2015",
//     email: "Lorem Ipsum",
//   },
//   {
//     id: "5",
//     name: "Lena Page",
//     role: "Admin",
//     createdDate: "8 Oct, 2016",
//     email: "Lorem Ipsum",
//   },
// ];

const page = () => {
  const {data:res,isLoading,isSuccess} = useGetManagersForAdminQuery({})
  const type1 = res?.data?.type1
  const type2 = res?.data?.type2
  const data = [type1, type2]
  
  

  return (
    <Box className="py-6 w-full bg-primarykey-background">
      <Text className="text-primary-default font-bold text-2xl mb-3">
        Managers Dashboard
      </Text>
      <Flex className="gap-3 items-center">
        <Input
          placeholder="Search"
          radius="md"
          w={350}
          leftSection={<IconSearch />}
        />

        <Button>Search</Button>

        <Menu>
          <Menu.Target>
            <Button
              variant="transparent"
              className="text-primary-text"
              rightSection={<IconChevronDown />}
            >
              Sort by
            </Button>
          </Menu.Target>

          <Menu.Dropdown>
            <Menu.Item>Items</Menu.Item>
          </Menu.Dropdown>
        </Menu>
        <Menu>
          <Menu.Target>
            <Button
              variant="transparent"
              className="text-primary-text"
              rightSection={<IconChevronDown />}
            >
              Saved Search
            </Button>
          </Menu.Target>

          <Menu.Dropdown>
            <Menu.Item>Items</Menu.Item>
          </Menu.Dropdown>
        </Menu>
        <IconAdjustmentsHorizontal className="cursor-pointer" />
      </Flex>
      {
        isSuccess && <RecentComplaints data={data} />
      }
      
    </Box>
  );
};

export default page;
