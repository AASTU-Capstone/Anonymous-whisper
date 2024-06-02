import { Box, Flex } from "@mantine/core";
import React from "react";
import Sidebar from "./_components/sidebar";
import Header from "@/shared/header";

export default function PublicLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <Flex>
      <Sidebar />
      <Box className="gap-5 flex flex-col px-10 py-5 w-full bg-primarykey-background">
        <Header role="Manager" />
        {children}
      </Box>
    </Flex>
  );
}
