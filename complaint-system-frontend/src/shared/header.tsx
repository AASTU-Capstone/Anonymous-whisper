"use client";
import { Avatar, Box, Divider, Flex, Text } from "@mantine/core";
import { IconBell, IconBellPlus, IconChevronDown } from "@tabler/icons-react";
import React from "react";
import jwt from "jsonwebtoken";

const Header = ({ role }: { role: string }) => {
  const notification = true;
  const token = decodeURIComponent(typeof window !== "undefined" ? document.cookie : "")
  .split(";")
  .find((c) => c.trim().startsWith("token="))
  ?.split("=")[1];
  const decodedToken: any = jwt.decode(token || "");
  var username = decodedToken?.useremail?.split("@")[0];
  const usertype = decodedToken?.typ
  const userid = decodedToken?.userid
  if (usertype === "subordinate"){
    //change name
  }
  else if(usertype === "manager"){

  }
  else if(usertype === "admin"){
    
  }

  return (
    <header>
      <Flex className="justify-between w-full">
        <Box>
          <Text className="font-bold">Hello, {username || "John"}</Text>
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
            <IconChevronDown />
          </Flex>
        </Flex>
      </Flex>
    </header>
  );
};

export default Header;
