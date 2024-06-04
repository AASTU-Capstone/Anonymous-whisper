import { FileWithPath } from "@mantine/dropzone";

export interface SignupCredentials {
  email: string;
  password: string;
  user_Type?: string;
}

export interface LoginCredentials {
  email: string;
  password: string;
}

export interface SignupApiResponse {
  id: string;
  message: string;
  success: boolean;
  error?: string[];
  statusCode: number;
}

export interface LoginApiResponse {
  success: boolean;
  message: string;
  id: string;
  email: string;
  token: string;
  isVerified: boolean;
}

export interface updateStartupProfileInput {
  problemStatement: string;
  proposedSolution: string;
  financialProjection: string;
  businessModel: string;
  marketAnalysis: string;
  competitor: string;
  pitchdeck: string;
  pitchVideo: string;
}

export interface updateStartupProfileApiResponse {
  id: string;
  message: string;
  statusCode: number;
  success: boolean;
  data: string;
  error: string[];
}

/////////////////////////////////////////////////////////////

export interface verifyAccountInput {
  email: string;
  OTPCode: string;
}

export interface verifyAccontApiResponse {
  id: string;
  message: string;
  success: boolean;
  error?: string[];
}

export interface forgotpasswordotp {
  email: string;
}

export interface forgotpasswordotpApiResponse {
  id: string;
  message: string;
  statusCode: number;
  success: boolean;
  data?: string;
  error?: string[];
}

export interface resetPassword {
  newPassword: string;
  email: string;
}

export interface resetPasswordApiResponse {
  id: string;
  message: string;
  statusCode: number;
  success: boolean;
  data?: string;
  error?: string[];
}

export interface createOTPInput {
  email: string;
}
export interface createOTPApiResponse {
  id: string;
  message: string;
  success: boolean;
  error?: string[];
}

export interface GetComplaintsResponse {
  id: string;
  message: string;
  statusCode: number;
  success: boolean;
  data?: string;
  error?: string[];
}

export interface GetSubordinatesResponse {
  id: string;
  name: string;
  email: string;
  mitigatedCount: Int32Array;
}

export interface CreateSubordinateInput {
  name: string;
  email: string;
}

export interface GetComplaintLogToAssignForManagerResponse {
  id: string;
  title: string;
  status: string;
  priority: string;
  createdAt: string;
}

export interface GetComplaintLogToUpdateForManagerResponse {
  id: string;
  title: string;
  priority: string;
  subordinate: string;
  manager: string;
  createdAt: string;
}

/////////////////////// admin  ////////////////////////
export interface AssignManagerInput {
  title: string;
  priority: string;
  managerId: string;
  complaintId: string;
}

export interface AddManagerInput {
  name: string;
  email: string;
  role: string;
}

export interface UpdateComplaintStatusInputForAdmin {
  complaintId: string;
  status: string;
}

export interface UpdateComplaintLogStatusInputForAdmin {
  complaintLogId: string;
  status: string;
}

export interface ManagerReponse {
  id: string;
  Name: string;
  Role: string;
  Email: string;
  CreatedAt: string;
}

//////////////////// subordinate ////////////////////
export interface AssignSubordinateInput {
  complaintLogId: string;
  subordinateId: string;
}

export interface GetComplaintsForUserResponse {
  id: string;
  title: string;
  status: string;
  createdAt: string;
  category: string;
}

export interface CreateComplaintInput {
  Title: string;
  Category: string | null;
  Content: string;
  ImagesEvidence: FileWithPath[];
  SoundTrack: FileWithPath[];
  Videos: FileWithPath[];
  Documents: FileWithPath[];
}

export interface UpdateComplaintLogStatusForSubordinate {
  complainLogId: string;
  status: string;
}
export interface updateComplaintLogReport {
  id: string;
  report: string;
}
