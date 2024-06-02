"use client";
import { Flex, Group, Modal, Text } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import {
  IconFileDescription,
  IconLayoutDashboard,
  IconUserHexagon,
} from "@tabler/icons-react";
import Link from "next/link";
import { useState } from "react";
import CreateComplaint from "./create-complaint";

const data = [
  { link: "/manager/dashboard", label: "Dashboard", icon: IconLayoutDashboard },
  {
    link: "/manager/complaint-log",
    label: "Complaint Log",
    icon: IconFileDescription,
  },
  {
    link: "/manager/subordinates",
    label: "Subordinates",
    icon: IconUserHexagon,
  },
];

function Sidebar() {
  const [active, setActive] = useState("");
  const [opened, { open, close }] = useDisclosure(false);

  const links = data.map((item) => (
    <Link href={item.link} key={item.label}>
      <Text
        onClick={() => {
          setActive(item.label);
        }}
        className={`flex min-h-11 w-full items-center gap-4 ${active === item.label ? "text-primary-default border-r-4 border-primary-default" : "opacity-55"} `}
      >
        <item.icon stroke={1.5} />
        {item.label}
      </Text>
    </Link>
  ));

  return (
    <>
      <Modal
        opened={opened}
        size="70%"
        onClose={close}
        title="Create Complaint"
      >
        <CreateComplaint closeModal={close} />
      </Modal>
      <Flex className="my-9 ml-10 h-screen w-72 flex-col">
        <Group className="mb-8" justify="space-between">
          <Text className="text-xl font-bold">Your Logo</Text>
        </Group>
        <Flex className="w-full flex-col gap-1">{links}</Flex>
      </Flex>
    </>
  );
}

export default Sidebar;
