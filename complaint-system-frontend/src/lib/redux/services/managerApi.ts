import {
  CreateSubordinateInput,
  AssignSubordinateInput,
  UpdateComplaintLogStatusInput,
  DeleteSubordinateInput,
} from "@/types";
import baseApi from "./baseApi";

const managerApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    // Get All Subordinates
    GetSubordinates: builder.query({
      query: () => `/Manager/GetSubordinates`,
    }),

    GetManagerProfile : builder.query({
      query:()=> `/Manager/GetProfile`
    }),

    // Create Subordinate
    CreateSubordinate: builder.mutation<any, CreateSubordinateInput>({
      query: (credentials: CreateSubordinateInput) => ({
        url: "/Manager/CreateSubordinate",
        method: "POST",
        body: credentials,
      }),
    }),

    // Get Complaint Log To Assign For Manager
    GetComplaintLogToAssignForManager: builder.query({
      query: () => `/Manager/GetComplaintLogToAssign`,
    }),

    // Get Complaint Log To Update For Manager
    GetComplaintLogToUpdateForManager: builder.query({
      query: () => `/Manager/GetComplaintLogToUpdate`,
    }),

    // Assign Subordinate
    AssignSubordinate: builder.mutation<any, AssignSubordinateInput>({
      query: (credentials: AssignSubordinateInput) => ({
        url: "/Manager/AssignSubordinate",
        method: "POST",
        body: credentials,
      }),
    }),

    // Update Complaint Log Status
    UpdateComplaintLogStatus: builder.mutation<
      any,
      UpdateComplaintLogStatusInput
    >({
      query: (input: UpdateComplaintLogStatusInput) => ({
        url: "/Manager/UpdateComplaintLogStatus",
        method: "PATCH",
        body: input,
      }),
    }),

    DeleteSubordinate : builder.mutation<any, DeleteSubordinateInput>({
      query:(deleteSubordinateInput:DeleteSubordinateInput) =>({
        url:`/Manager/DeleteSubordinate`,
        method:"DELETE",
        body:deleteSubordinateInput
      })
    })

    // SearchSubordinates: builder.query({
    //   query: (keyword) => `/Manager/SearchSubordinates?Keyword=${keyword}`,
    // }),
  }),
});

export default managerApi;
