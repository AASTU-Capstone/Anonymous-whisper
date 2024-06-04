import { Box } from "@mantine/core";
import RecentComplaints from "./table";

export interface Data {
  id: string;
  title: string;
  priority: string;
  manager: string;
  createdDate: string;
}

const page = () => {
  return (
    <Box className="w-full bg-primary-background">
      <RecentComplaints data={data} />
    </Box>
  );
};

export default page;
