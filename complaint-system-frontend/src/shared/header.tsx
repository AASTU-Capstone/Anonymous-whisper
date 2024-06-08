"use client";
import { ActionIcon, Avatar, Box, Divider, Flex, Menu, Text } from "@mantine/core";
import Badge from '@mui/material/Badge';
import {IconBell, IconChevronDown} from "@tabler/icons-react";
import { useGetAdminProfileQuery } from "@/lib/redux/features/admin";
import { useGetManagerProfileQuery } from "@/lib/redux/features/manager";
import { useGetSubordinateProfileQuery } from "@/lib/redux/features/subordinate";
import React, { useEffect, useRef, useState } from "react";
import jwt from "jsonwebtoken";
import Link from "next/link";
import { toast } from "react-toastify";
import { useAuth } from "@/hooks/useAuth";
import { useRouter } from "next/router";
import { useWebSocket } from "@/providers/WebSocketContext";
import NotificationArea from "@/shared/notificationArea";

interface Notification {
  Sender: string;
  Date: string;
  Message: string;
  unread: boolean;
}

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
  const { messages, logout } = useWebSocket();
  const handleSignOut = () => {
    logoutHandler();
    logout();
    notify();
  };

  const notification = true;
  const token = decodeURIComponent(
    typeof window !== "undefined" ? document.cookie : ""
  )
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


  // notification setup
  const [showNotifications, setShowNotifications] = useState(false);
  const [notifications, setNotifications] = useState<Notification[]>([]);

  
  useEffect(() => {
    if (messages.length > 0) {
      console.log('recieved', messages)
      const sortedMessages = messages.sort((a: Notification, b: Notification) => new Date(b.Date).getTime() - new Date(a.Date).getTime());
      setNotifications(sortedMessages);
    }
  }, [messages]);

  const toggleNotifications = () => {
    setShowNotifications((prev) => !prev);
    if (!showNotifications && notifications.some((notification) => notification.unread)) {
      setNotifications((prevNotifications) =>
        prevNotifications.map((notification) => ({
          ...notification
        }))
      );
    }
  };

  
  const notificationRef = useRef<HTMLDivElement>(null);

  const handleClickOutside = (event: MouseEvent) => {
    if (notificationRef.current && !notificationRef.current.contains(event.target as Node)) {
      setShowNotifications(false);
    }
  };

  useEffect(() => {
    if (showNotifications) {
      document.addEventListener("mousedown", handleClickOutside);
    } else {
      document.removeEventListener("mousedown", handleClickOutside);
    }
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [showNotifications]);

  return (
    <header>
      <Flex className="justify-between w-full">
        <Box>
          <Text className="font-bold">Hello, {firstName || "John"}</Text>
          <Text c="dimmed" className="text-sm">
            Have a nice day
          </Text>
        </Box>
          <Flex className="items-center gap-3 justify-center relative" ref={notificationRef}>
            <ActionIcon
              onClick={toggleNotifications}
              size="lg"
              style={{
                color: "#757575",
                backgroundColor: "#fff",
                borderRadius: "50%",
                position: "relative"
              }}
            >
              <IconBell />
            </ActionIcon>
            {!showNotifications && notifications.some((notification) => notification.unread) && (
              <Badge badgeContent={notifications.filter((notification) => notification.unread).length} color="primary" style={{ position: "relative", top: -12, right: 12 }}>
                {/* Place content here if needed */}
              </Badge>
            )}
            {showNotifications && <NotificationArea notifications={notifications} />}
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
                <Menu.Item
                  component={Link}
                  href="/reset-password/change"
                  className="menu-item-hover-blue"
                >
                  <Text className="text-primary-default py-2 font-bold ">
                    Reset Password
                  </Text>
                </Menu.Item>
                <Menu.Item
                  component={Link}
                  href="/login"
                  className="menu-item-hover-alert"
                  onClick={handleSignOut}
                >
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
