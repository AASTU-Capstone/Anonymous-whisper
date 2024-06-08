"use client";
import DataTable from "@/shared/table";
import {
  ActionIcon,
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
import { useDisclosure } from "@mantine/hooks";
import {
  IconAdjustmentsHorizontal,
  IconChevronDown,
  IconSquareX,
  IconPlus,
  IconSearch,
} from "@tabler/icons-react";
import { useMemo } from "react";
import { Column } from "react-table";
import { useState } from "react";
import { useCreateSubordinateMutation, useDeleteSubordinateMutation } from "@/lib/redux/features/manager";
import { GetSubordinatesResponse, CreateSubordinateInput, DeleteSubordinateInput } from "@/types";
import { modals } from "@mantine/modals";

const SubordinatesList = ({
  data,
  refetchSubordinate,
}: {
  data: GetSubordinatesResponse[];
  refetchSubordinate: () => void;
}) => {
  const [opened, { open, close }] = useDisclosure(false);
  const [searchQuery, setSearchQuery] = useState("");
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [createSubordinate] = useCreateSubordinateMutation();
  const[deleteSubordinate] = useDeleteSubordinateMutation();
  const[subordinateId, setSubordinateId] = useState("")

  const [nameError, setNameError] = useState("");
  const [emailError, setEmailError] = useState("");

  const filteredData = useMemo(() => {
    return data.filter((item: GetSubordinatesResponse) =>
      item.name.toLowerCase().includes(searchQuery.toLowerCase())
    );
  }, [searchQuery, data]);

  const handleAddSubordinate = async () => {
     // Reset error messages
     setNameError("");
     setEmailError("");
 
     // Validate inputs
     let valid = true;
     if (!name) {
       setNameError("Please enter a name.");
       valid = false;
     }
     if (!email) {
       setEmailError("Please enter an email.");
       valid = false;
     }
 
     if (!valid) {
       return;
     }

    const newSubordinate: CreateSubordinateInput = {
      name,
      email,
    };

    try {
      await createSubordinate(newSubordinate).unwrap();
      refetchSubordinate();
      setName("");
      setEmail("");
      close();
    } catch (error) {
      console.error("Failed to add subordinate: ", error);
    }
  };

  const deleteSubordinateHandler = async (value:string)=>{
    console.log(value)
    setSubordinateId(value)
    modals.openConfirmModal({
      title: "Delete Subordinate",
      centered: true,
      children: (
        <Text size="sm">Are you sure you want to Delete the Subordinate?</Text>
      ),
      labels: { confirm: "Delete Subordinate", cancel: "Cancel" },
      confirmProps: { color: "red" },
      closeOnConfirm: true,
      onConfirm: async () => {
        const deleteSubordinateInput : DeleteSubordinateInput = {
          id:value,
        }
        try{
          await deleteSubordinate(deleteSubordinateInput)
          refetchSubordinate();
          return;
        }catch(error){
          console.error("Failed to delete subordinate: ", error);
        }
      },
    });

   
  }

  const columns: Array<Column<GetSubordinatesResponse>> = useMemo(
    () => [
      {
        Header: "Name",
        accessor: "name",
        Cell: ({ value }) => (
          <div className="text-sm font-medium text-gray-900">{value}</div>
        ),
      },
      {
        Header: "Email",
        accessor: "email",
      },

      {
        Header: "Mitigated Count",
        accessor: "mitigatedCount",
        Cell: ({ value }) => <div className="pl-12">{value}</div>,
      },
      {
        Header: "Action",
        accessor: 'id',
        Cell: ({ value }) => {
          return (
            <div className="flex space-x-4">
              <ActionIcon variant="light">
                <button
                  onClick={(e)=>deleteSubordinateHandler(value)}
                  className="text-gray-500 hover:text-gray-700"
                >
                  <IconSquareX color= "red" className="w-5 h-5" />
                </button>
              </ActionIcon>
            </div>
          )
        }
      }
    ],
    []
  );

  return (
    <>
      <Modal centered opened={opened} onClose={close} title="Add Subordinate">
        <TextInput
          placeholder="Full Name"
          className="mb-3"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        {nameError && <span className="text-red-500 text-sm">{nameError}</span>}
        <TextInput
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        {emailError && <span className="text-red-500 text-sm">{emailError}</span>}
        <Group justify="end" className="mt-7">
          <Button onClick={handleAddSubordinate}>Add</Button>
          <Button variant="light" onClick={close}>
            Cancel
          </Button>
        </Group>
      </Modal>
      <Text className="text-primary-default font-bold text-2xl mb-5">
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
      <Box className="w-full mt-7">
        <Box>
          <Text className="text-xl px-5 py-4 bg-primary-body">
            Subordinates List
          </Text>
        </Box>

        <DataTable columns={columns} data={filteredData} pageSize={5} />
      </Box>
    </>
  );
};

export default SubordinatesList;
