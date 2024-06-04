import { 
  GetComplaintsResponse, 
  UpdateComplaintLogStatusForSubordinate,
  updateComplaintLogReport } 
  from "@/types";
import baseApi from "./baseApi";

const subordinateApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        GetComplaintsToUpdateForSubordinate: builder.query({
            query: () => `/Subordinate/GetComplaintLogsToUpdate`,
          }),

        GetComplaintByIdForSubordinate : builder.query<any, string>({
          query:(complaintLogId:string)=> `/ComplaintLog/GetComplaintLogById?ComplaintLogId=${complaintLogId}`,
        }),

        UpdateComplaintLogStatusForSubordinate : builder.mutation<any, UpdateComplaintLogStatusForSubordinate>({
          query:(complaintLog:UpdateComplaintLogStatusForSubordinate)=>({
            url : `/Subordinate/UpdateComplaintLogStatus`,
            method:"PATCH",
            body:complaintLog
          }),
        }),

        UpdateComplaintLogReportForSubordinate : builder.mutation<any, updateComplaintLogReport>({
          query:(complaintLogReport:updateComplaintLogReport)=>({
            url:`/Subordinate/UpdateComplaintLog`,
            method:"PATCH",
            body:complaintLogReport
          }),
        }),

        }),
})

export default subordinateApi;