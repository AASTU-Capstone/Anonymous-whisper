"use client";
import {
  Box,
  Button,
  Flex,
  Group,
  Input,
  Menu,
  Modal,
  Text,
  TextInput,
} from "@mantine/core";
import RecentComplaints from "./table";
import { useMemo } from "react";
import {
  IconAdjustmentsHorizontal,
  IconChevronDown,
  IconPlus,
  IconSearch,
} from "@tabler/icons-react";
import { useDisclosure } from "@mantine/hooks";

export interface Data {
  id: string;
  name: string;
  email: string;
  mitigatedCount: string;
}

const data: Data[] = [
  {
    id: "1",
    name: "David Wagner",
    email: "Lorem Ipsum",
    mitigatedCount: "3",
  },
  {
    id: "2",
    name: "Ina Hogan",
    email: "Lorem Ipsum",
    mitigatedCount: "4",
  },
  {
    id: "3",
    name: "Devin Harmon",
    mitigatedCount: "1",
    email: "Lorem Ipsum",
  },
  {
    id: "4",
    name: "Lee Harmon",
    mitigatedCount: "6",
    email: "Lorem Ipsum",
  },
  {
    id: "5",
    name: "Lena Page",
    mitigatedCount: "8",
    email: "Lorem Ipsum",
  },
];

const page = () => {
  const [opened, { open, close }] = useDisclosure(false);

  return (
    <>
      <Modal centered opened={opened} onClose={close} title="Add Subordinate">
        <TextInput placeholder="Full Name" className="mb-3" />
        <TextInput placeholder="Email" />
        <Group justify="end" className="mt-7">
          <Button>Add</Button>
          <Button variant="light" onClick={close}>
            Cancel
          </Button>
        </Group>
      </Modal>
      <Box className="py-3 w-full bg-primarykey-background">
        <Text className="text-primary-default font-bold text-2xl mb-3">
          Subordinate Dashboard
        </Text>
        <Flex className="gap-3 items-center">
          <Input
            placeholder="Search"
            radius="md"
            w={350}
            leftSection={<IconSearch />}
          />

          <Button rightSection={<IconPlus />} onClick={open}>
            Add Subordinate
          </Button>

          <Menu>
            <Menu.Target>
              <Button
                variant="transparent"
                className="text-primary-text"
                rightSection={<IconChevronDown />}
              >
                Sort by
              </Button>
            </Menu.Target>

            <Menu.Dropdown>
              <Menu.Item>Items</Menu.Item>
            </Menu.Dropdown>
          </Menu>
          <Menu>
            <Menu.Target>
              <Button
                variant="transparent"
                className="text-primary-text"
                rightSection={<IconChevronDown />}
              >
                Saved Search
              </Button>
            </Menu.Target>

            <Menu.Dropdown>
              <Menu.Item>Items</Menu.Item>
            </Menu.Dropdown>
          </Menu>
          <IconAdjustmentsHorizontal className="cursor-pointer" />
        </Flex>

        <RecentComplaints data={data} />
      </Box>
    </>
  );
};

export default page;
