import { CreateSubordinateInput } from "@/types";
import baseApi from "./baseApi";

const managerApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    GetSubordinates: builder.query({
      query: () => `/Manager/GetSubordinates`,
    }),

    SearchSubordinates: builder.query({
      query: (keyword) => `/Manager/SearchSubordinates?Keyword=${keyword}`,
    }),

    // Create Subordinate
    CreateSubordinate: builder.mutation<any, CreateSubordinateInput>({
      query: (credentials: CreateSubordinateInput) => ({
        url: "/Manager/CreateSubordinate",
        method: "POST",
        body: credentials,
      }),
    }),

    GetComplaintLogToAssignForManager: builder.query({
      query: () => `/Manager/GetComplaintLogToAssign`,
    }),
  }),
});

export default managerApi;
