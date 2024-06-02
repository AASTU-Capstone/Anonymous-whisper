"use client";
import React, { useEffect, useState } from "react";

interface FormData {
  email: string;
  password: string;

}
import {
  MdOutlinePassword,
  MdOutlineMailOutline,
  MdOutlineRemoveRedEye,
} from "react-icons/md";
import { FcGoogle } from "react-icons/fc";
import { FaRegEyeSlash, FaLinkedin } from "react-icons/fa";
import Link from "next/link";
// import { useAuth } from "@/hooks/useAuth";
// import { useRouter } from "next/navigation";
// import { useDispatch } from "react-redux";
// import { useSignupMutation } from "@/lib/redux/features/user";
// import { setEmail } from "@/lib/redux/slices/authSlice";
// import { Dispatch } from "@reduxjs/toolkit";

type Credentials = {
  email: string;
  password: string;
};

type Props = {};

export default function SignUp({}: Props) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState<string>("");
  const [confirmPassword, setConfirmPassword] = useState<string>("");
  const [isVisible, setIsVisible] = useState<boolean>(false);
  const [isConfirmVisible, setIsConfirmVisible] = useState<boolean>(false);
  const [isError, setError] = useState("");
  // const [signup, { isSuccess, isLoading, error }] = useSignupMutation();

  // const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
  //   event.preventDefault();

  // };

  // const {
  //   auth: { isLoading, error },
  //   signupHandler,
  // } = useAuth();

  // const router = useRouter();
  // const dispatch = useDispatch();

  const handleSignup = async (credentials: {email: any, password: any, user_Type: any}) => {
    // try {
    //   let payload = await signup(credentials).unwrap();
    //   console.log(payload);
    //   if (payload.success == true){
    //     router.push("/auth/signup/verify-otp");
    //   } else {
    //     setError(payload.error);
    //   }
      
    // } catch (error) {
    //   console.log(error);
      
    // }
  };

  // useEffect(() => {
  //   if (!isLoading && !error) {
  //     router.push("/auth/reset-password");
  //   }
  // }, [isLoading, error]);
    

  const handleSubmit = (ev: any) => {
    ev.preventDefault();
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (email === "" || email === null) {
      setError("Email is required");
      return;
    }
    if (!emailRegex.test(email)) {
      setError("Email is not valid");
      return;
    }
    if (
      password === "" ||
      password === null ||
      confirmPassword === "" ||
      confirmPassword === null
    ) {
      setError("Password and Confirm Password are required");
      return;
    }
    if (password !== confirmPassword) {
      setError("Password and Confirm Password do not match");
      return;
    }
    sessionStorage.setItem("email", email);
    handleSignup({ email, password, user_Type: "startup"});
    // console.log(isSuccess, isLoading, error);
  };

  return (
    <div className="h-full w-full flex items-center justify-center font-[Poppins]">
      <div className="w-2/3 gap-4">
        <h1 className="text-2xl text-center font-roboto pb-4 text-black  mb-3">
          CREATE ACCOUNT
        </h1>

        {/* Login Form */}
        <form
          action="POST"
          onSubmit={handleSubmit}
          className="gap-1 flex flex-col space-y-1 mb-3"
        >
          {/* Email */}
          <div className="relative">
            <MdOutlineMailOutline className="absolute left-3 top-[14px] font-light text-sm" />
            <input
              type="email"
              placeholder="Your Email"
              className="text-xs py-3 leading-4 border border-blue-200 w-full pl-10 rounded-lg  px-3  focus:outline-none focus:ring-1 focus:ring-blue-300"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>

          {/* Password */}
          <div className="relative">
            <MdOutlinePassword className="absolute left-3 top-[14px] font-light text-sm" />
            <input
              type={isVisible ? "text" : "password"}
              placeholder="Password"
              className="text-xs py-3 leading-4 border border-blue-200 w-full pl-10 rounded-lg  px-3  focus:outline-none focus:ring-1 focus:ring-blue-300 "
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            {isVisible ? (
              <MdOutlineRemoveRedEye
                name="password"
                className="text-gray-400 cursor-pointer absolute top-[9px] right-2 text-[19px]"
                onClick={() => setIsVisible(false)}
              />
            ) : (
              <FaRegEyeSlash
                className="absolute top-[9px] cursor-pointer right-2 text-[19px] text-gray-400"
                onClick={() => setIsVisible(true)}
              />
            )}
          </div>

          <div className="relative">
            <MdOutlinePassword className="absolute left-3 top-[14px] font-light text-sm" />
            <input
              type={isConfirmVisible ? "text" : "password"}
              placeholder="Confirm Password"
              className="text-xs py-3 leading-4 border border-blue-200 w-full pl-10 rounded-lg  px-3  focus:outline-none focus:ring-1 focus:ring-blue-300"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
            {isConfirmVisible ? (
              <MdOutlineRemoveRedEye
                className="text-gray-400 cursor-pointer absolute top-[9px] right-2 text-2xl"
                onClick={() => setIsConfirmVisible(false)}
              />
            ) : (
              <FaRegEyeSlash
                className="absolute top-[9px] cursor-pointer right-2 text-[19px] text-gray-400"
                onClick={() => setIsConfirmVisible(true)}
              />
            )}
          </div>

          {/* error message */}
          {isError && <div className="text-red-500 text-sm">{isError}</div>}

          <button
            className="bg-[#3563E9] font-medium text-lg border-transparent py-2 text-white cursor-pointer hover:bg-custom-blue/75 transition duration-150 ease-linear rounded-lg my-3"
            type="submit"
            // disabled={isLoading}
          >
            {/* {isLoading ? (
              <span>Processing . . .</span>
            ) : ( */}
              <span>Create Account</span>
            {/* )} */}
          </button>
        </form>


        <div className="flex flex-row items-center w-full my-4 mx-auto">
          <div className="flex-grow bg-gray-600 h-[0.5px] rounded-2xl"></div>
          <div className="px-5 text-gray-600 text-lg">or</div>
          <div className="flex-grow bg-gray-600 h-[0.5px] rounded-2xl"></div>
        </div>

        <div className="space-y-4">
          <button className="rounded-lg py-3 bg-white text-white w-full flex justify-center items-center border-none cursor-pointer">
            <span className="flex flex-row items-center">
              <FcGoogle className="m-1 mx-5 text-2xl" />
              <div className="text-gray-900 text-lg font-medium">
                Continue with Google
              </div>
            </span>
          </button>
          {/* <button className="bg-gray-200 rounded-lg py-3 text-white w-full flex justify-center items-center border-none cursor-pointer">
            <span className="flex flex-row items-center">
              <FaLinkedin className="m-1 mx-5 text-blue-600 text-2xl" />
              <div className="text-gray-900 text-md text-lg font-medium">
                Continue with LinkedIn
              </div>
            </span>
          </button> */}
        </div>
      </div>
    </div>
  );
}
