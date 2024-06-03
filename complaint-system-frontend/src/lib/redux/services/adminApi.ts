import { METHODS } from "http";
import baseApi from "./baseApi";
import {AssignManagerInput} from "@/types/index"
import { url } from "inspector";

const AdminApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        GetComplaintLogsToUpdateForAdmin: builder.query({
            query: () => `/Admin/GetComplaintLogsToUpdate`,
          }),

          GetRecievedComplaintsForAdmin: builder.query({
            query:() => `/Admin/GetRecievedComplaints`,
          }),

          GetAcceptedComplaintsForAdmin: builder.query({
            query:()=> `/Admin/GetAcceptedComplaints`,
          }),

          GetManagersForAdmin: builder.query({
            query:()=> `/Admin/GetManagers`,
          }),

          GetAllComplaintsForAdmin: builder.query({
            query:()=> `/Admin/GetAllComplaints`,
          }),

          AssignManagerForAdmin : builder.mutation<any, AssignManagerInput>({
            query:(assignSubordinate:AssignManagerInput)=>({
              url: `/Admin/AssignManagers`,
              method:"Post",
              body:assignSubordinate,
            })
          }),
            
          
        }),
        
})

export default AdminApi;