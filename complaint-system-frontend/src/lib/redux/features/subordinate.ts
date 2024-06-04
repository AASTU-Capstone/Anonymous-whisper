import subordinateApi from "../services/subordinateApi";
export const {
    useGetComplaintsToUpdateForSubordinateQuery,
    useGetComplaintByIdForSubordinateQuery,
    useUpdateComplaintLogStatusForSubordinateMutation,
    useUpdateComplaintLogReportForSubordinateMutation
} = subordinateApi;