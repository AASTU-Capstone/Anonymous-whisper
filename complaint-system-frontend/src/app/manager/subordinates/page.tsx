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
import { useMemo, useState } from "react";
import {
  IconAdjustmentsHorizontal,
  IconChevronDown,
  IconPlus,
  IconSearch,
} from "@tabler/icons-react";
import { useDisclosure } from "@mantine/hooks";
import {
  useGetSubordinatesQuery,
  useCreateSubordinateMutation,
} from "@/lib/redux/features/manager";
import { GetSubordinatesResponse, CreateSubordinateInput } from "@/types";


const Subordinates = () => {
  const [opened, { open, close }] = useDisclosure(false);
  const {
    data: res,
    isLoading,
    isSuccess,
    refetch,
  } = useGetSubordinatesQuery({});
  const [searchQuery, setSearchQuery] = useState("");
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [createSubordinate] = useCreateSubordinateMutation();

  const data =
    res?.data?.map((item: GetSubordinatesResponse) => {
      return {
        ...item,
      };
    }) || [];

  const filteredData = useMemo(() => {
    return data.filter((item: GetSubordinatesResponse) =>
      item.name.toLowerCase().includes(searchQuery.toLowerCase())
    );
  }, [searchQuery, data]);

  const handleAddSubordinate = async () => {
    if (!name || !email) {
      alert("Please fill in all fields");
      return;
    }

    const newSubordinate: CreateSubordinateInput = {
      name,
      email,
    };

    try {
      const result = await createSubordinate(newSubordinate).unwrap();
      refetch();
      setName("");
      setEmail("");
      close();
    } catch (error) {
      console.error("Failed to add subordinate: ", error);
      alert("Failed to add subordinate");
    }
  };

  return (
    <>
      <Modal centered opened={opened} onClose={close} title="Add Subordinate">
        <TextInput
          placeholder="Full Name"
          className="mb-3"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <TextInput
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <Group justify="end" className="mt-7">
          <Button onClick={handleAddSubordinate}>Add</Button>
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
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
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

        <RecentComplaints data={filteredData} />
      </Box>
    </>
  );
};

export default Subordinates;
