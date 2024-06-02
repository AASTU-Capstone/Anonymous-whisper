import * as Yup from "yup";
// import {
//   locations,
//   StartupStages,
//   startupIndustries,
//   courses,
// } from "../utils/SelectOptions";

// const passwordRules = !/(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*[0-9]).{7,}/
const passwordRules =   /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_\-+={}[\]|:;"'<>,.?/~`]).*$/;

export const SignUpValidation = Yup.object().shape({
  email: Yup.string()
    .email("please enter a valid email!")
    .required("Email is required!"),
  password: Yup.string()
    .matches(
      passwordRules,
      "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character."
    )
    .min(7, "password must be at least 7 characters.")
    .required("password is required!!"),
  confirmPassword: Yup.string()
    .oneOf([Yup.ref("password"), null as any], "Passwords must match")
    .required("Confirm password is required"),
});

export const LoginValidation = Yup.object().shape({
  email: Yup.string()
    .email("please enter a valid email!")
    .required("Email is required!"),
  // course: Yup.string()
  //   .oneOf(courses.map((course) => course.id))
  //   .required("course is required"),
  password: Yup.string()
    .matches(
      passwordRules,
      "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character."
    )
    .min(6, "password must be at least 6 characters.")
    .required("password is required!!"),
});

export const ForgotPasswordValidation = Yup.object().shape({
  email: Yup.string()
    .email("please enter a valid email!")
    .required("Email is required!"),
});

export const ResetPasswordValidation = Yup.object().shape({
  newpassword: Yup.string()
    .matches(
      passwordRules,
      "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character."
    )
    .min(6, "password must be at least 6 characters.")
    .required("password is required!!"),
  confirmPassword: Yup.string()
    .oneOf([Yup.ref("newpassword"), null as any], "Passwords must match")
    .required("Required"),
});

export const ProfileCreationValidation = Yup.object().shape({
  ImageLogo: Yup.mixed()
    .test('file', 'profile picture is required', value => value instanceof File)
    .required("profile picture is required"),
  StartupName: Yup.string().required("Company name is required"),
  Industry: Yup.array()
    .of(Yup.string())
    .min(1, 'Please select at least one option')
    .required('Industry is required'),
  // Location: Yup.string().oneOf(locations.map((location) => location.label)).required("Location is required"),
  FoundingDate: Yup.string().required("Founding date is required"),
  // StartupStage: Yup.string()
  //   .oneOf(StartupStages.map((stage) => stage.value))
  //   .required("Startup stage is required"),
  StartupOverview: Yup.string()
    .max(1000)
    .required("Startup overview is required"),
});

export const BasicInfoValidation = Yup.object().shape({
  ImageLogo: Yup.mixed().required("profile picture is required"),
  CoverImage: Yup.mixed().required("cover picture is required"),
  Industry: Yup.array()
    .of(Yup.string())
    .min(1, 'Please select at least one option')
    .required('Industry is required'),
  StartupName: Yup.string().required("Company name is required"),
  // .datetime('Invalid date format')
  FoundingDate: Yup.string().required("Founding date is required"),
  // StartupStage: Yup.string()
  //   .oneOf(StartupStages.map((stage) => stage.value))
  //   .required("Startup stage is required"),
  StartupOverview: Yup.string()
    .max(1000)
    .required("Startup overview is required"),
});

const linkedinRegex = /^(https:\/\/www.linkedin.com\/)/;
const calendlyRegex = /^(https:\/\/calendly.com\/)/;

export const ContactInfoValidation = Yup.object().shape({
  phone: Yup.string()
    .length(10, "Phone number must be 10 digits")
    .required("Phone number is required"),
  url: Yup.string().url("Invalid URL"),
  account: Yup.string()
    .required("Linkedin account is required")
    .matches(linkedinRegex, "Invalid URL"),
  calendly: Yup.string()
    .required("Calendly link is required")
    .matches(calendlyRegex, "Invalid URL"),
  // location: Yup.string().oneOf(locations.map((location) => location.label)).required("Location is required"),
});

export const TeamMemberValidation = Yup.object().shape({
  // profilePic:
  // name:
  // role:
  // about:
});

// Start up Achievement Validation
