import { Box, Text, Divider, ScrollArea, Flex, Badge, Indicator } from "@mantine/core";
import { useState } from "react";

const NotificationArea = ({ notifications }) => {
    const [displayedNotifications, setDisplayedNotifications] = useState(notifications);

  const handleNotificationClick = (index) => {
    const updatedNotifications = [...displayedNotifications];
    updatedNotifications[index].unread = false;
    setDisplayedNotifications(updatedNotifications);
  };


  return (
    <Box
      className="absolute right-0 top-12 z-50 bg-white shadow-lg rounded-lg p-4 transform transition-transform duration-300"
      style={{
        width: "450px",
        border: "1px solid #e0e0e0",
        boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)"
      }}
    >
      <Flex align="center" justify="space-between" mb="sm">
        <Text weight={700} size="lg">Notifications</Text>
      </Flex>
      <Divider my="sm" />
      <ScrollArea style={{ height: "250px" }}>
        {notifications.length > 0 ? (
          notifications.map((notification, index) => (
            <Box key={index} my="sm" onClick={() => handleNotificationClick(index)} style={{
                cursor: "pointer",
                backgroundColor: notification.unread ? "#f5f6f7" : "transparent",
                padding: "11px",
                borderRadius: "4px"
              }}>
              <Flex align="center" justify="space-between">
                <Text size="md" color="dark">
                  {notification.message}
                </Text>
                {notification.unread && 
                // <Badge color="blue" variant="dot" />}
                <Indicator
    size={8} // Adjust the size as needed
    style={{
      backgroundColor: "#2196f3", // or any other color you prefer
      marginLeft: "8px", // Adjust the spacing as needed
    }}
  />}
              </Flex>
            </Box>
          ))          
          ) : (
            <Flex align="center" justify="center" style={{ height: "100%" }}>
            <Text size="sm" c="dimmed">
              No new notifications
            </Text>
          </Flex>
        )}
        {notifications.length > 0 && (

            <Divider my="sm" />
        )}
      </ScrollArea>
    </Box>
  );
};

export default NotificationArea;
