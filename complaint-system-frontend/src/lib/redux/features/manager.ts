import managerApi from "../services/managerApi";
export const {
  useGetSubordinatesQuery,
  useSearchSubordinatesQuery,
  useCreateSubordinateMutation,
  useGetComplaintLogToAssignForManagerQuery,
  useAssignSubordinateMutation,
} = managerApi;
