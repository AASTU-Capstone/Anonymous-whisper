import baseApi from "./baseApi";

const notificationApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    MarkNotifications: builder.mutation<any, any>({
        query: (IDs: any) => ({
            url: "/Notification/MarkNotificationsToRead",
            method: "POST",
            body: IDs
        })
    }),
    
    GetUnreadNotifications: builder.query({
        query: () => "/Notification/GetUnreadNotifications"
    })
  })
})

export default notificationApi;