import AdminApi from "../services/adminApi";
export const {
    useGetComplaintLogsToUpdateForAdminQuery,
    useGetRecievedComplaintsForAdminQuery,
    useGetAcceptedComplaintsForAdminQuery,
    useGetManagersForAdminQuery,
    useGetAllComplaintsForAdminQuery,
    useAssignManagerForAdminMutation
} = AdminApi;