"use client"
import React from "react";
import { FaRegCircleCheck } from "react-icons/fa6";
import { useRouter } from "next/navigation";

type Props = {}; 

export default function Success({}: Props) {
  const router = useRouter();
  const handleOnClick = () => {
    // Navigate to the setup profile page
  }
  return (
    <div className = "flex justify-center items-center h-full">
      <div className="flex flex-col justify-center items-center w-2/3 gap-2">
        <FaRegCircleCheck className="text-8xl font-light text-[#3563E9] m-2"/>
        <h1 className="mb-3 text-4xl">Successfully Verified</h1>
        <button className="text-white bg-[#3563E9] rounded-lg text-sm px-7 py-3" onClick={handleOnClick}>Setup Your Profile</button>
      </div>
    </div>
  );
}