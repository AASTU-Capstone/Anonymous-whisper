import { METHODS } from "http";
import baseApi from "./baseApi";
import {
  AssignManagerInput,
  AddManagerInput,
  UpdateComplaintStatusInputForAdmin,
  UpdateComplaintLogStatusInputForAdmin,
  EditManagerInput,
} from "@/types/index";
import { url } from "inspector";

const AdminApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    GetAdminProfile : builder.query({
      query:()=> `/Admin/GetProfile`
    }),

    GetComplaintLogsToUpdateForAdmin: builder.query({
      query: () => `/Admin/GetComplaintLogsToUpdate`,
    }),

    GetRecievedComplaintsForAdmin: builder.query({
      query: () => `/Admin/GetRecievedComplaints`,
    }),

    GetAcceptedComplaintsForAdmin: builder.query({
      query: () => `/Admin/GetAcceptedComplaints`,
    }),

    GetManagersForAdmin: builder.query({
      query: () => `/Admin/GetManagers`,
    }),

    GetAllComplaintsForAdmin: builder.query({
      query: () => `/Admin/GetAllComplaints`,
    }),

    GetComplaintByIdForAdmin: builder.query<any, string>({
      query: (complaintId: string) =>
        `/Admin/GetComplaintById?ComplaintID=${complaintId}`,
    }),

    AssignManagerForAdmin: builder.mutation<any, AssignManagerInput>({
      query: (assignSubordinate: AssignManagerInput) => ({
        url: `/Admin/AssignManagers`,
        method: "POST",
        body: assignSubordinate,
      }),
    }),

    AddManagerForAdmin: builder.mutation<any, AddManagerInput>({
      query: (addmanager: AddManagerInput) => ({
        url: `/Admin/CreateManagers`,
        method: "POST",
        body: addmanager,
      }),
    }),

    UpdateManagerForAdmin : builder.mutation<any, EditManagerInput>({
      query:(editManagerInput:EditManagerInput)=>({
        url: `/Admin/UpdateManager`,
        method: "PATCH",
        body:editManagerInput
      })
    }),

    UpdateComplaintStatusForAdmin: builder.mutation<
      any,
      UpdateComplaintStatusInputForAdmin
    >({
      query: (updateComplaint: UpdateComplaintStatusInputForAdmin) => ({
        url: `/Admin/UpdateComplaintStatus`,
        method: "PATCH",
        body: updateComplaint,
      }),
    }),

    UpdateComplaintLogStatusForAdmin: builder.mutation<
      any,
      UpdateComplaintLogStatusInputForAdmin
    >({
      query: (updateComplaintLog: UpdateComplaintLogStatusInputForAdmin) => ({
        url: `/Admin/UpdateReportStatus`,
        method: "PATCH",
        body: updateComplaintLog,
      }),
    }),
  }),
});

export default AdminApi;
