"use client";
import { Box, Button, Flex, Input, Menu, Text } from "@mantine/core";
import RecentComplaints from "./table";
import { useMemo, useState } from "react";
import {
  IconAdjustmentsHorizontal,
  IconChevronDown,
  IconSearch,
} from "@tabler/icons-react";
import { GetComplaintLogToAssignForManagerResponse } from "@/types";
import { useGetComplaintLogToAssignForManagerQuery } from "@/lib/redux/features/manager";

const ComplaintsLog = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetComplaintLogToAssignForManagerQuery({});

  const [searchQuery, setSearchQuery] = useState("");
  console.log(res);

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
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
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

      <RecentComplaints data={filteredData} />
    </Box>
  );
};

export default ComplaintsLog;
