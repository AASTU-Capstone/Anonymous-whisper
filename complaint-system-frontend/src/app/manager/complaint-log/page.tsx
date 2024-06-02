"use client";
import { Box, Button, Flex, Input, Menu, Text } from "@mantine/core";
import RecentComplaints from "./table";
import { useMemo } from "react";
import {
  IconAdjustmentsHorizontal,
  IconChevronDown,
  IconSearch,
} from "@tabler/icons-react";

export interface Data {
  id: string;
  title: string;
  priority: string;
  manager: string;
  createdDate: string;
}

const data: Data[] = [
  {
    id: "1",
    title: "David Wagner",
    priority: "high",
    createdDate: "24 Oct, 2015",
    manager: "Lorem Ipsum",
  },
  {
    id: "2",
    title: "Ina Hogan",
    priority: "medium",
    createdDate: "24 Oct, 2015",
    manager: "Lorem Ipsum",
  },
  {
    id: "3",
    title: "Devin Harmon",
    priority: "low",
    createdDate: "18 Dec, 2015",
    manager: "Lorem Ipsum",
  },
  {
    id: "4",
    title: "Lena Page",
    priority: "medium",
    createdDate: "8 Oct, 2016",
    manager: "Lorem Ipsum",
  },
  {
    id: "5",
    title: "Eula Horton",
    priority: "high",
    createdDate: "15 Jun, 2017",
    manager: "Lorem Ipsum",
  },
  {
    id: "6",
    title: "Victoria Perez",
    priority: "high",
    createdDate: "12 Jan, 2019",
    manager: "Lorem Ipsum",
  },
  {
    id: "7",
    title: "Cora Medina",
    priority: "low",
    createdDate: "21 July, 2020",
    manager: "Lorem Ipsum",
  },
];

const ComplaintsLog = () => {
  return (
    <Box className="py-3 w-full bg-primarykey-background">
      <Text className="text-primary-default font-bold text-2xl mb-5">
        Complaints
      </Text>
      <Flex className="gap-3 mb-5 items-center">
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

      <RecentComplaints data={data} />
    </Box>
  );
};

export default ComplaintsLog;
