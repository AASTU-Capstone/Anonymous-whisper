import { FilePicker } from "@/shared/dropzone";
import {
  Button,
  Flex,
  Group,
  MultiSelect,
  TextInput,
  Textarea,
} from "@mantine/core";
import React from "react";

type props = {
  closeModal: () => void;
};

const CreateComplaint = ({ closeModal }: props) => {
  return (
    <Flex className="flex-col gap-4 w-full">
      <TextInput placeholder="Title" className="w-1/2" required />
      <MultiSelect
        placeholder="Category"
        data={["React", "Angular", "Vue", "Svelte"]}
      />
      <Textarea placeholder="Content goes here" autosize minRows={5} />

      <FilePicker />
      <Group className="justify-end mt-4">
        <Button>Create</Button>
        <Button variant="light" onClick={closeModal}>
          Cancel
        </Button>
      </Group>
    </Flex>
  );
};

export default CreateComplaint;
