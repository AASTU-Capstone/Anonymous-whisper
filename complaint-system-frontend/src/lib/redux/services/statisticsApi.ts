import baseApi from "./baseApi";

const StatisticsApi = baseApi.injectEndpoints({
    endpoints:(builder)=>({
        GetComplaintStatitistics: builder.query({
            query:()=>`/Statistics/GetComplaintStatistics`
          }),
    })
})

export default StatisticsApi;