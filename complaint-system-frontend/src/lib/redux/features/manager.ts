import managerApi from "../services/managerApi";
export const {
  useGetSubordinatesQuery,
  useUpdateComplaintLogStatusMutation,
  useCreateSubordinateMutation,
  useGetComplaintLogToAssignForManagerQuery,
  useGetComplaintLogToUpdateForManagerQuery,
  useAssignSubordinateMutation,
  useGetManagerProfileQuery
} = managerApi;
