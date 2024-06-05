"use client";
import { Avatar, Box, Divider, Flex, Menu, Text } from "@mantine/core";
import { IconBell, IconBellPlus, IconChevronDown } from "@tabler/icons-react";
import { useGetAdminProfileQuery } from "@/lib/redux/features/admin";
import { useGetManagerProfileQuery } from "@/lib/redux/features/manager";
import { useGetSubordinateProfileQuery } from "@/lib/redux/features/subordinate";
import React from "react";
import jwt from "jsonwebtoken";
import Link from "next/link";
import { toast } from "react-toastify";
import { useAuth } from "@/hooks/useAuth";
import { useRouter } from "next/router";

const notify = () => {
  toast.success("Logout Successful", {
    position: "bottom-center",
    autoClose: 3000, // Set the timeout to 2 seconds (2000 milliseconds)
    hideProgressBar: true,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "colored",
    style: {
      color: "#fff",
      backgroundColor: "#3563E9",
      padding: "0px",
    },
  });
};

const Header = ({ role }: { role: string }) => {
  const { logoutHandler } = useAuth();
  const handleSignOut = () => {
    logoutHandler();
    notify();
  };

  const notification = true;
  const token = decodeURIComponent(typeof window !== "undefined" ? document.cookie : "")
    .split(";")
    .find((c) => c.trim().startsWith("token="))
    ?.split("=")[1];
  const decodedToken: any = jwt.decode(token || "");
  const usernameFromEmail = decodedToken?.useremail?.split("@")[0];
  const usertype = decodedToken?.typ;

  const { data: subData } = useGetSubordinateProfileQuery({});
  const { data: managerData } = useGetManagerProfileQuery({});
  const { data: adminData } = useGetAdminProfileQuery({});

  let username = usernameFromEmail;
  let firstName = username;

  if (usertype === "subordinate" && subData) {
    username = subData?.data?.name;
    firstName = username?.split(" ")[0];
  } else if (usertype === "manager" && managerData) {
    username = managerData?.data?.name;
    firstName = username?.split(" ")[0];
  } else if (usertype === "admin" && adminData) {
    username = adminData?.data?.name;
    firstName = username?.split(" ")[0];
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
              <Text>{username || "John Doe"}</Text>
              <Text c="dimmed" className="text-sm">
                {role}
              </Text>
            </Box>
            <Menu width={200} shadow="md">
              <Menu.Target>
                <IconChevronDown className="cursor-pointer icon-hover" />
              </Menu.Target>

              <Menu.Dropdown>
              <Menu.Item component={Link} href="/reset-password/change" className="menu-item-hover-blue">
                  <Text className="text-primary-default py-2 font-bold ">
                    Reset Password
                  </Text>
                </Menu.Item>
                <Menu.Item component={Link} href="/login" className="menu-item-hover-alert" onClick={handleSignOut}>
                  <Text className="text-primary-default py-2 font-bold ">
                    Log out
                  </Text>
                </Menu.Item>
                
              </Menu.Dropdown>
            </Menu>
          </Flex>
        </Flex>
      </Flex>
    </header>
  );
};

export default Header;
