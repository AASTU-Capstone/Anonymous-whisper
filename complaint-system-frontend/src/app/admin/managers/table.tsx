"use client";
import DataTable from "@/shared/table";
import {
  Box,
  Button,
  Flex,
  Group,
  Input,
  Menu,
  Modal,
  Select,
  Text,
  TextInput,
} from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import {
  IconAdjustmentsHorizontal,
  IconChevronDown,
  IconPlus,
  IconSearch,
} from "@tabler/icons-react";
import { useMemo, useState } from "react";
import { Column } from "react-table";
import { ManagerResponse,AddManagerInput } from "@/types";
import {useAddManagerForAdminMutation} from "@/lib/redux/features/admin";

const ManagersList = ({ data, refetchManagers }: { data: ManagerResponse[]; refetchManagers: () => void; }) => {
  const [opened, { open, close }] = useDisclosure(false);
  const [searchQuery, setSearchQuery] = useState("");
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const[role, setRole] = useState<string | null>(null);
  const [createManager] = useAddManagerForAdminMutation();

  
  const handleAddManager = async () => {
    if (!name || !email || !role) {
      alert("Please fill in all fields");
      return;
    }

    const newManager: AddManagerInput = {
      name,
      email,
      role,
    };

    try {
      await createManager(newManager).unwrap();
      refetchManagers();
      setName("");
      setEmail("");
      setRole("");
      close();
    } catch (error) {
      console.error("Failed to add manager: ", error);
      // alert("Failed to add manager");
    }
  };

  const columns: Array<Column<ManagerResponse>> = useMemo(
    () => [
      {
        Header: "Name",
        accessor: "name",
        Cell: ({ value }) => (
          <div className="text-sm font-medium text-gray-900">{value}</div>
        ),
      },
      {
        Header: "Role",
        accessor: "role",
        Cell: ({ value }) => {
          const statusClass =
            value === "Employee"
              ? "bg-gray-200 text-gray-500"
              : "bg-blue-500 text-white";
          return (
            <span
              className={`py-1 px-5 text-center text-xs leading-5 font-semibold rounded-full ${statusClass}`}
            >
              {value}
            </span>
          );
        },
      },
      {
        Header: "Created Date",
        accessor: "createdAt",
      },
      {
        Header: "Email",
        accessor: "email",
      },
    ],
    []
  );

  return (
    <>
      <Modal centered opened={opened} onClose={close} title="Add Managers">
        <Flex className="flex-col my-5 gap-7">
          <TextInput placeholder="Full Name" required value={name}
          onChange={(e) => setName(e.target.value)}/>
          <TextInput placeholder="Email" required value={email}
          onChange={(e) => setEmail(e.target.value)}/>
          <Select
            placeholder="Select Role Type"
            data={["premitigation", "postmitigation"]}
            value={role}
            onChange={(value) => setRole(value)}
          />
        </Flex>
        <Group justify="end">
          <Button onClick={handleAddManager}>Add Manager</Button>
          <Button variant="light" onClick={close}>
            Cancel
          </Button>
        </Group>
      </Modal>

      <Text className="text-primary-default font-bold text-2xl mb-3">
        Managers Dashboard
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
          Add Manager
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
      <Box className="w-full mt-7">
        <Box>
          <Text className="text-xl px-5 py-4 bg-primary-body">
            Managers List
          </Text>
        </Box>

        <DataTable columns={columns} data={data} pageSize={5} />
      </Box>
    </>
  );
};

export default ManagersList;
