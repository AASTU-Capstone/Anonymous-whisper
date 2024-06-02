import { Avatar, Box, Divider, Flex, Text } from "@mantine/core";
import { IconBell, IconBellPlus, IconChevronDown } from "@tabler/icons-react";
import React from "react";

const Header = ({ role }: { role: string }) => {
  const notification = true;

  return (
    <header>
      <Flex className="justify-between w-full">
        <Box>
          <Text className="font-bold">Hello, John</Text>
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
              <Text>John deo</Text>
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
