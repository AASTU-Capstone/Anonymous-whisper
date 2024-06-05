"use client";
import { Avatar, Box, Button, Divider, Flex, Menu, Text } from "@mantine/core";
import { IconBell, IconBellPlus, IconChevronDown } from "@tabler/icons-react";
import { useGetAdminProfileQuery } from "@/lib/redux/features/admin";
import { useGetManagerProfileQuery } from "@/lib/redux/features/manager";
import { useGetSubordinateProfileQuery } from "@/lib/redux/features/subordinate";
import React from "react";
import jwt from "jsonwebtoken";
import Link from "next/link";

const Header = ({ role }: { role: string }) => {
  const notification = true;
  const token = decodeURIComponent(typeof window !== "undefined" ? document.cookie : "")
  .split(";")
  .find((c) => c.trim().startsWith("token="))
  ?.split("=")[1];
  const decodedToken: any = jwt.decode(token || "");
  var username = decodedToken?.useremail?.split("@")[0];
  var firstName = username
  const usertype = decodedToken?.typ
  if (usertype === "subordinate"){
    //change name
    var {data:res,isLoading:subLoading,isSuccess:subSuccess} = useGetSubordinateProfileQuery({})
    username = res?.data?.name
    firstName = username?.split(" ")[0]
  }
  else if(usertype === "manager"){
    var {data:res,isLoading:managerLoading,isSuccess:managerSuccess} = useGetManagerProfileQuery({})
    username = res?.data?.name
    firstName = username?.split(" ")[0]
  }
  else if(usertype === "admin"){
    var {data:res,isLoading:adminLoading,isSuccess:adminSuccess} = useGetAdminProfileQuery({})
    username = res?.data?.name
    firstName = username?.split(" ")[0]
  }

  return (
    <header>
      <Flex className="justify-between w-full">
        <Box>
          <Text className="font-bold">Hello, {firstName || "John"}</Text>
          <Text c="dimmed" className="text-sm">
            Have a nice day
          </Text>
        </Box>
        <Flex className="items-center gap-3 justify-center">
          <Box className="relative">
            {notification ? <IconBellPlus /> : <IconBell />}
          </Box>
          <Divider orientation="vertical" />
          <Flex className="items-center gap-3 justify-center">
            <Avatar />
            <Box>
              <Text>{username ||"John Doe"}</Text>
              <Text c="dimmed" className="text-sm">
                {role}
              </Text>
            </Box>
            {/* <IconChevronDown /> */}
          </Flex>
          <Menu width={200} shadow="md">
            <Menu.Target>
              {/* <Button variant="transparent" className="p-0"> */}
              <IconChevronDown className="cursor-pointer icon-hover" />
              {/* </Button> */}
            </Menu.Target>

            <Menu.Dropdown>
              <Menu.Item component={Link} href="/signup" className="menu-item-hover">
                <Text className="text-primary-default py-2 font-bold " >
                  Log out
                </Text>
              </Menu.Item>
            </Menu.Dropdown>
          </Menu>
        </Flex>
      </Flex>
    </header>
  );
};

export default Header;
