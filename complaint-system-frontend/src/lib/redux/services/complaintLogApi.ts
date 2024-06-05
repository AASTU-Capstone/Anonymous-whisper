import baseApi from "./baseApi";
const ComplaintLogApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        GetComplaintLogById : builder.query<any, string>({
            query:(complaintLogId:string)=> `/ComplaintLog/GetComplaintLogById?ComplaintLogId=${complaintLogId}`,
          }),
    })
})

export default ComplaintLogApi