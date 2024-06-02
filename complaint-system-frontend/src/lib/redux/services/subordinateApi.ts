import { GetComplaintsResponse } from "@/types";
import baseApi from "./baseApi";

const subordinateApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        GetComplaintsToUpdateForSubordinate: builder.query({
            query: () => `/Subordinate/GetComplaintLogsToUpdate`,
          }),

        }),
})

export default subordinateApi;