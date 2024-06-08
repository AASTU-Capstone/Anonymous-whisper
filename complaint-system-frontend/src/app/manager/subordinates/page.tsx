"use client";
import { Box } from "@mantine/core";
import SubordinatesTable from "./table";
import { useGetSubordinatesQuery } from "@/lib/redux/features/manager";
import { GetSubordinatesResponse } from "@/types";
import { useWebSocket } from "@/providers/WebSocketContext";
import { useEffect } from "react";

const page = () => {
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetSubordinatesQuery({});

  const webSocketContext = useWebSocket();

  // Check if webSocketContext is available
  if (!webSocketContext) {
    return <div>Loading...</div>;
  }

  const { messages, sendMessage, logout } = webSocketContext;

  useEffect(() => {
    // console.log("WebSocket Messages: ", messages);
    console.log("here we go: ", messages);
  }, [messages]);

  const data =
    res?.data?.map((item: GetSubordinatesResponse) => {
      return {
        ...item,
      };
    }) || [];

  return (
    <>
      <Box className="w-full bg-primary-background">
        <SubordinatesTable data={data} refetchSubordinate={refetch} />
      </Box>
    </>
  );
};

export default page;
