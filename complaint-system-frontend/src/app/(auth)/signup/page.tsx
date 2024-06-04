// "use client";
// import React, { useEffect, useState } from "react";
// import { FcGoogle } from "react-icons/fc";
// import { FaRegEyeSlash, FaLinkedin } from "react-icons/fa";
// import Link from "next/link";
// import { AiOutlineMail } from "react-icons/ai";
// import { useAuth } from "@/hooks/useAuth";
// import { useRouter } from "next/navigation";
// import { useDispatch } from "react-redux";
// import { getAuth } from "firebase/auth";
// import { signInWithPopup, GoogleAuthProvider } from "firebase/auth";
// import { toast } from "react-toastify";
// import Image from "next/image";
// import { resetAuth } from "@/lib/redux/slices/authSlice";
// import { useFormik } from "formik";
// import { SignUpValidation } from "@/utils/schema";
// import {
//   MdOutlinePassword,
//   MdOutlineMailOutline,
//   MdOutlineRemoveRedEye,
// } from "react-icons/md";

// interface FormData {
//   email: string;
//   password: string;
// }

// const notify = () => {
//   toast.success("OTP sent, check your email", {
//     position: "bottom-center",
//     autoClose: 7000,
//     hideProgressBar: true,
//     closeOnClick: true,
//     pauseOnHover: true,
//     draggable: true,
//     progress: undefined,
//     theme: "colored",
//     style: {
//       color: "#fff",
//       backgroundColor: "#3563E9",
//       padding: "0px",
//     },
//   });
// };

// // import { useAuth } from "@/hooks/useAuth";
// // import { useRouter } from "next/navigation";
// // import { useDispatch } from "react-redux";
// // import { useSignupMutation } from "@/lib/redux/features/user";
// // import { setEmail } from "@/lib/redux/slices/authSlice";
// // import { Dispatch } from "@reduxjs/toolkit";

// type Credentials = {
//   email: string;
//   password: string;
// };

// type Props = {};

// export default function SignUp({}: Props) {
//   const [isVisible, setIsVisible] = useState<boolean>(false);
//   const [isConfirmVisible, setIsConfirmVisible] = useState<boolean>(false);
//   const {
//     signupHandler,
//     auth: { isAuthenticated, isLoading, error },
//   } = useAuth();
//   const [Email, setEmail] = useState<string>("");

//   const { values, errors, touched, handleSubmit, handleChange } = useFormik({
//     initialValues: {
//       email: "",
//       password: "",
//       confirmPassword: "",
//     },
//     validationSchema: SignUpValidation,
//     onSubmit: async (values, actions) => {
//       if (values.email && values.password) {
//         const credentials = {
//           email: values.email,
//           password: values.password,
//           user_Type: "startup",
//         };
//         const res = await signupHandler(credentials);
//         if (res && "data" in res) {
//           //
//           if (!res.data.success) {
//           }
//           if (res.data.success) {
//             notify();
//             router.push(`/auth/signup/verify-otp?email=${values.email}`);
//           } else {
//             toast.error(res.data.error as any);
//           }
//         }
//         actions.resetForm();
//       } else {
//         toast.error("All fields are required");
//       }
//     },
//   });

//   const router = useRouter();
//   const dispatch = useDispatch();

//   // const handleSignup = async (credentials: {
//   //   email: any;
//   //   password: any;
//   //   user_Type: any;
//   // }) => {
//   //   // try {
//   //   //   let payload = await signup(credentials).unwrap();
//   //   //   console.log(payload);
//   //   //   if (payload.success == true){
//   //   //     router.push("/auth/signup/verify-otp");
//   //   //   } else {
//   //   //     setError(payload.error);
//   //   //   }
//   //   // } catch (error) {
//   //   //   console.log(error);
//   //   // }
//   // };

//   // useEffect(() => {
//   //   if (!isLoading && !error) {
//   //     router.push("/auth/reset-password");
//   //   }
//   // }, [isLoading, error]);

//   // const handleSubmit = (ev: any) => {
//   //   ev.preventDefault();
//   //   const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
//   //   if (email === "" || email === null) {
//   //     setError("Email is required");
//   //     return;
//   //   }
//   //   if (!emailRegex.test(email)) {
//   //     setError("Email is not valid");
//   //     return;
//   //   }
//   //   if (
//   //     password === "" ||
//   //     password === null ||
//   //     confirmPassword === "" ||
//   //     confirmPassword === null
//   //   ) {
//   //     setError("Password and Confirm Password are required");
//   //     return;
//   //   }
//   //   if (password !== confirmPassword) {
//   //     setError("Password and Confirm Password do not match");
//   //     return;
//   //   }
//   //   sessionStorage.setItem("email", email);
//   //   handleSignup({ email, password, user_Type: "user" });
//   //   // console.log(isSuccess, isLoading, error);
//   // };

//   return (
//     <div className="h-full w-full flex items-center justify-center font-[Poppins]">
//       <div className="w-2/3 gap-4">
//         <h1 className="text-2xl text-center font-roboto pb-4 text-black  mb-3">
//           CREATE ACCOUNT
//         </h1>

//         {/* Login Form */}
//         <form
//           action="POST"
//           onSubmit={handleSubmit}
//           className="gap-1 flex flex-col space-y-1 mb-3"
//         >
//           {/* Email */}
//           <div className="relative">
//             <MdOutlineMailOutline className="absolute left-3 top-[14px] font-light text-sm" />
//             <input
//               type="email"
//               placeholder="Your Email"
//               className="text-xs py-3 leading-4 border border-blue-200 w-full pl-10 rounded-lg  px-3  focus:outline-none focus:ring-1 focus:ring-blue-300"
//               value={email}
//               onChange={(e) => setEmail(e.target.value)}
//             />
//           </div>

//           {/* Password */}
//           <div className="relative">
//             <MdOutlinePassword className="absolute left-3 top-[14px] font-light text-sm" />
//             <input
//               type={isVisible ? "text" : "password"}
//               placeholder="Password"
//               className="text-xs py-3 leading-4 border border-blue-200 w-full pl-10 rounded-lg  px-3  focus:outline-none focus:ring-1 focus:ring-blue-300 "
//               value={password}
//               onChange={(e) => setPassword(e.target.value)}
//             />
//             {isVisible ? (
//               <MdOutlineRemoveRedEye
//                 name="password"
//                 className="text-gray-400 cursor-pointer absolute top-[9px] right-2 text-[19px]"
//                 onClick={() => setIsVisible(false)}
//               />
//             ) : (
//               <FaRegEyeSlash
//                 className="absolute top-[9px] cursor-pointer right-2 text-[19px] text-gray-400"
//                 onClick={() => setIsVisible(true)}
//               />
//             )}
//           </div>

//           <div className="relative">
//             <MdOutlinePassword className="absolute left-3 top-[14px] font-light text-sm" />
//             <input
//               type={isConfirmVisible ? "text" : "password"}
//               placeholder="Confirm Password"
//               className="text-xs py-3 leading-4 border border-blue-200 w-full pl-10 rounded-lg  px-3  focus:outline-none focus:ring-1 focus:ring-blue-300"
//               value={confirmPassword}
//               onChange={(e) => setConfirmPassword(e.target.value)}
//             />
//             {isConfirmVisible ? (
//               <MdOutlineRemoveRedEye
//                 className="text-gray-400 cursor-pointer absolute top-[9px] right-2 text-2xl"
//                 onClick={() => setIsConfirmVisible(false)}
//               />
//             ) : (
//               <FaRegEyeSlash
//                 className="absolute top-[9px] cursor-pointer right-2 text-[19px] text-gray-400"
//                 onClick={() => setIsConfirmVisible(true)}
//               />
//             )}
//           </div>

//           {/* error message */}
//           {isError && <div className="text-red-500 text-sm">{isError}</div>}

//           <button
//             className="bg-[#3563E9] font-medium text-lg border-transparent py-2 text-white cursor-pointer hover:bg-custom-blue/75 transition duration-150 ease-linear rounded-lg my-3"
//             type="submit"
//             // disabled={isLoading}
//           >
//             {/* {isLoading ? (
//               <span>Processing . . .</span>
//             ) : ( */}
//             <span>Create Account</span>
//             {/* )} */}
//           </button>
//         </form>

//         <div className="flex flex-row items-center w-full my-4 mx-auto">
//           <div className="flex-grow bg-gray-600 h-[0.5px] rounded-2xl"></div>
//           <div className="px-5 text-gray-600 text-lg">or</div>
//           <div className="flex-grow bg-gray-600 h-[0.5px] rounded-2xl"></div>
//         </div>

//         <div className="space-y-4">
//           <button className="rounded-lg py-3 bg-white text-white w-full flex justify-center items-center border-none cursor-pointer">
//             <span className="flex flex-row items-center">
//               <FcGoogle className="m-1 mx-5 text-2xl" />
//               <div className="text-gray-900 text-lg font-medium">
//                 Continue with Google
//               </div>
//             </span>
//           </button>
//           {/* <button className="bg-gray-200 rounded-lg py-3 text-white w-full flex justify-center items-center border-none cursor-pointer">
//             <span className="flex flex-row items-center">
//               <FaLinkedin className="m-1 mx-5 text-blue-600 text-2xl" />
//               <div className="text-gray-900 text-md text-lg font-medium">
//                 Continue with LinkedIn
//               </div>
//             </span>
//           </button> */}
//         </div>
//       </div>
//     </div>
//   );
// }

"use client";

import { useState } from "react";
import { MdOutlinePassword, MdOutlineRemoveRedEye } from "react-icons/md";
import { FcGoogle } from "react-icons/fc";
import { FaRegEyeSlash, FaLinkedin } from "react-icons/fa";
import { AiOutlineMail } from "react-icons/ai";
import Link from "next/link";
import { useAuth } from "@/hooks/useAuth";
import { useRouter } from "next/navigation";
import { useDispatch } from "react-redux";
import { getAuth } from "firebase/auth";
import { signInWithPopup, GoogleAuthProvider } from "firebase/auth";
import { toast } from "react-toastify";
import Image from "next/image";
import { resetAuth } from "@/lib/redux/slices/authSlice";
import { useFormik } from "formik";
import { SignUpValidation } from "@/utils/schema";

const notify = () => {
  toast.success("OTP sent, check your email", {
    position: "bottom-center",
    autoClose: 7000,
    hideProgressBar: true,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "colored",
    style: {
      color: "#fff",
      backgroundColor: "#3563E9",
      padding: "0px",
    },
  });
};
export default function SignUp() {
  const [isVisible, setIsVisible] = useState<boolean>(false);
  const [isConfirmVisible, setIsConfirmVisible] = useState<boolean>(false);
  const {
    signupHandler,
    auth: { isAuthenticated, isLoading, error },
  } = useAuth();
  const [Email, setEmail] = useState<string>("");

  const { values, errors, touched, handleSubmit, handleChange } = useFormik({
    initialValues: {
      email: "",
      password: "",
      confirmPassword: "",
    },
    validationSchema: SignUpValidation,
    onSubmit: async (values, actions) => {
      if (values.email && values.password) {
        const credentials = {
          email: values.email,
          password: values.password,
          user_Type: "user",
        };
        const res = await signupHandler(credentials);
        if (res && "data" in res) {
          if (!res.data.success) {
          }
          if (res.data.success) {
            notify();
            router.push(`/signup/verify-otp?email=${values.email}`);
          } else {
            toast.error(res.data.error as any);
          }
        }
        actions.resetForm();
      } else {
        toast.error("All fields are required");
      }
    },
  });

  const router = useRouter();
  const dispatch = useDispatch();

  // const handleSignup = async (email: string): Promise<void> => {
  //   const res = await signupHandler({
  //     email: email,
  //     password: "Biruk@12345678",
  //     user_Type: "startup",
  //   });
  //   if (res && "data" in res) {
  //     //
  //     if (!res.data.success) {
  //     }
  //     if (res.data.success) {
  //       notify();
  //       router.push(`/auth/signup/verify-otp?email=${email}`);
  //     } else {
  //       toast.error(res.data.error as any);
  //     }
  //   }
  // };

  // const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  return (
    //@ts-ignore
    <div className="h-full w-full flex items-center justify-center font-[Poppins]">
      <div className="w-2/3 gap-4">
        <h1 className="text-2xl text-center font-roboto pb-4 text-black  mb-3">
          CREATE ACCOUNT
        </h1>
        {error && error.data && (
          <div className="w-full border border-red-600/50 text-red-600 rounded-lg py-2.5 text-sm font-light flex items-center space-x-3 mb-3 pl-3">
            <Image
              src="/images/sign-error.svg"
              width={20}
              height={20}
              alt="error sign"
              className="object-contain cursor-pointer"
              onClick={() => dispatch(resetAuth())}
            />

            <p>
              {error?.status === "FETCH_ERROR"
                ? "Network Error"
                : error?.data.error}
            </p>
          </div>
        )}
        {/* Signup Form */}
        <form
          onSubmit={handleSubmit}
          className="flex flex-col space-y-2 mb-3 text-secondary"
          noValidate
        >
          {/* Email */}

          <div className="relative">
            <AiOutlineMail className="absolute left-3 top-[14px] font-light text-sm" />
            <input
              type="email"
              value={values.email}
              onChange={handleChange("email")}
              placeholder="Your Email"
              className={`text-xs py-3 leading-4 border border-blue-200 w-full pl-10 rounded-lg  px-3  focus:outline-none focus:ring-1 focus:ring-blue-300 ${
                errors.email && touched.email && "border-red-500"
              } `}
            />
            {/* error message */}
            {errors.email && touched.email && (
              <p className="text-red-500 text-12 font-light">{errors.email}</p>
            )}
          </div>

          {/* Password */}
          <div className="relative">
            <MdOutlinePassword className="absolute left-3 top-[12px] font-light text-sm" />
            <input
              type={isVisible ? "text" : "password"}
              value={values.password}
              onChange={handleChange("password")}
              placeholder="Password"
              className={`text-xs py-3 border-blue-200 leading-4 border w-full outline-none pl-10 rounded-lg px-3  focus:outline-none focus:ring-1 focus:ring-blue-300 ${
                errors.password && touched.password && "border-red-500"
              } `}
            />
            {isVisible ? (
              <MdOutlineRemoveRedEye
                name="password"
                className="cursor-pointer absolute top-3 right-[15px] text-sm"
                onClick={() => setIsVisible(false)}
              />
            ) : (
              <FaRegEyeSlash
                className="absolute top-3 cursor-pointer right-[15px] text-sm"
                onClick={() => setIsVisible(true)}
              />
            )}
            {/* error message */}
            {errors.password && touched.password && (
              <p className="text-red-500 text-12 font-light">
                {errors.password}
              </p>
            )}
          </div>

          {/* confirm password */}
          <div className="relative">
            <MdOutlinePassword className="absolute left-3 top-[12px] font-light text-sm" />
            <input
              type={isConfirmVisible ? "text" : "password"}
              value={values.confirmPassword}
              onChange={handleChange("confirmPassword")}
              placeholder="Confirm Password"
              className={`text-xs border-blue-200 py-3 leading-4 border w-full outline-none pl-10 rounded-lg px-3  focus:outline-none focus:ring-1 focus:ring-blue-00 ${
                errors.confirmPassword &&
                touched.confirmPassword &&
                "border-red-500"
              } `}
            />
            {isConfirmVisible ? (
              <MdOutlineRemoveRedEye
                className="cursor-pointer absolute top-3 right-[15px] text-sm"
                onClick={() => setIsConfirmVisible(false)}
              />
            ) : (
              <FaRegEyeSlash
                className="absolute top-3 cursor-pointer right-[15px] text-sm"
                onClick={() => setIsConfirmVisible(true)}
              />
            )}
            {/* error message */}
            {errors.confirmPassword && touched.confirmPassword && (
              <p className="text-red-500 text-12 font-light">
                {errors.confirmPassword}
              </p>
            )}
          </div>

          <button
            className="bg-[#3563E9] font-medium text-lg border-transparent py-2 text-white cursor-pointer hover:bg-custom-blue/75 transition duration-150 ease-linear rounded-lg my-3"
            type="submit"
            disabled={isLoading}
          >
            {isLoading ? (
              <div className="flex items-center justify-center gap-x-3 bg-transparent">
                <div className="spinner"></div>
                <span>Processing . . .</span>
              </div>
            ) : (
              <span>Create Account</span>
            )}
          </button>
        </form>
        <div className="flex flex-row items-center w-full my-4 mx-auto">
          <div className="flex-grow bg-gray-600/25 h-[1px] "></div>
          <div className="px-5 text-gray-600 ">or</div>
          <div className="flex-grow bg-gray-600/25 h-[1px] "></div>
        </div>
        <div className="gap-y-4 pt-2">
          <button className=" py-2  text-white w-full flex justify-center items-center border-none cursor-pointer hover:bg-background rounded-3xl">
            <div className="flex flex-row items-center">
              <FcGoogle className="m-1 mx-5 text-base" />
              <span className="pr-2 text-gray-900 text-sm font-medium">
                Continue with Google
              </span>
            </div>
          </button>
        </div>
        <div className="text-center pt-2 mt-3">
          <div className="text-12 flex justify-center items-center">
            <p className="text-black">I have an account?</p>
            <Link href="/auth/login" className=" font-medium pl-2">
              <span className="text-blue-500 hover:underline">Sign in</span>
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
}
