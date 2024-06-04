import subordinateApi from "../services/subordinateApi";
export const {
    useGetComplaintLogsToUpdateForSubordinateQuery,
    useGetComplaintLogByIdForSubordinateQuery,
    useUpdateComplaintLogStatusForSubordinateMutation,
    useUpdateComplaintLogReportForSubordinateMutation
} = subordinateApi;