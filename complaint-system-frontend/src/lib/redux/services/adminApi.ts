import baseApi from "./baseApi";

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
          
        }),
        
})

export default AdminApi;