"use client";
import React, { createContext, useContext, useEffect, useState } from "react";

const WebSocketContext = createContext<{
  messages: any[];
  sendMessage: (message: string) => void;
  logout: () => void;
} | null>(null);

export const WebSocketProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [socket, setSocket] = useState<WebSocket | null>(null);
  const [messages, setMessages] = useState<any[]>([]);

  useEffect(() => {
    const userId = localStorage.getItem("userId");
    if (userId) {
      const ws = new WebSocket(
        `ws://localhost:5097/notification?userId=${userId}`
      );

      ws.onopen = () => {
        console.log("WebSocket connection opened");
      };

      ws.onmessage = (event) => {
        const message = event.data;
        setMessages((prevMessages) => [...prevMessages, message]);
      };

      ws.onclose = () => {
        console.log("WebSocket connection closed");
      };

      setSocket(ws);

      return () => {
        ws.close();
      };
    }
  }, []);

  const sendMessage = (message: string) => {
    if (socket && socket.readyState === WebSocket.OPEN) {
      socket.send(message);
    }
  };

  const logout = () => {
    if (socket) {
      socket.close();
    }
    localStorage.removeItem("userId");
  };

  return (
    <WebSocketContext.Provider value={{ messages, sendMessage, logout }}>
      {children}
    </WebSocketContext.Provider>
  );
};

export const useWebSocket = () => useContext(WebSocketContext);
