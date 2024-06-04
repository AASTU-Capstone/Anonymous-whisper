import baseApi from "./baseApi";

const StatisticsApi = baseApi.injectEndpoints({
    endpoints:(builder)=>({
        GetComplaintStatitistics: builder.query<any,string>({
            query:(userId:string)=>`/Statistics/GetComplaintStatistics?UserId=${userId}`
          }),
    })
})

export default StatisticsApi;