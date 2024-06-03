import { FilePicker } from "@/shared/dropzone";
import { FileWithPath } from "@mantine/dropzone";
import React, { useState } from "react";
import { useCreateComplaintMutation } from "@/lib/redux/features/user";
import { CreateComplaintInput } from "@/types";
import {
  Button,
  Flex,
  Group,
  Select,
  TextInput,
  Textarea,
} from "@mantine/core";

type props = {
  closeModal: () => void;
};

const CreateComplaint = ({ closeModal }: props) => {
  const [title, setTitle] = useState("");
  const [category, setCategory] = useState<string | null>(null);
  const [CreateComplaintAction] = useCreateComplaintMutation();
  const [content, setContent] = useState("");
  const [files, setFiles] = useState<{
    images: FileWithPath[];
    audio: FileWithPath[];
    documents: FileWithPath[];
  }>({
    images: [],
    audio: [],
    documents: [],
  });

  const handleFilesSelected = (selectedFiles: {
    images: FileWithPath[];
    audio: FileWithPath[];
    documents: FileWithPath[];
  }) => {
    setFiles(selectedFiles);
  };

  const handleSubmit = async () => {
    console.log("submitting form");
    const formData: CreateComplaintInput = {
      title,
      category,
      content,
      images: files.images,
      audio: files.audio,
      video: [],
      documents: files.documents,
    };

    console.log("formData", formData);

    try {
      const response = await CreateComplaintAction(formData).unwrap();
      console.log("response", response);
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      closeModal();
    } catch (error) {
      console.error("Error submitting form:", error);
    }
  };

  return (
    <Flex className="flex-col gap-4 w-full">
      <TextInput
        placeholder="Title"
        className="w-1/2"
        required
        value={title}
        onChange={(e) => setTitle(e.currentTarget.value)}
      />
      <Select
        placeholder="Category"
        data={["bribe", "corruption", "harassment", "discrimination"]}
        value={category}
        onChange={(value) => {
          setCategory(value);
        }}
      />
      <Textarea
        placeholder="Content goes here"
        autosize
        minRows={5}
        value={content}
        onChange={(e) => setContent(e.currentTarget.value)}
      />

      <FilePicker onFilesSelected={handleFilesSelected} />
      <Group className="justify-end mt-4">
        <Button onClick={handleSubmit}>Create</Button>
        <Button variant="light" onClick={closeModal}>
          Cancel
        </Button>
      </Group>
    </Flex>
  );
};

export default CreateComplaint;
