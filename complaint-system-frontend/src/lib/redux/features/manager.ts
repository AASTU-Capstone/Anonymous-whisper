import managerApi from "../services/managerApi";
export const {
  useGetSubordinatesQuery,
  useSearchSubordinatesQuery,
  useCreateSubordinateMutation,
  useGetComplaintLogToAssignForManagerQuery,
  useGetComplaintLogToUpdateForManagerQuery,
  useAssignSubordinateMutation,
} = managerApi;
