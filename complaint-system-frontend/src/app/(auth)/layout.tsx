"use client";

import Carousel from "@/components/auth/Slider";
// import NextNProgress from "nextjs-progressbar";

export default function AuthLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <main className="h-screen">
      <div className="grid lg:grid-cols-2 grid-cols-1 h-full">
        <div className="bg-background hidden lg:block">
        {/* bg-[#3563E9] */}
          <Carousel />
        </div>
        <div className="overflow-y-scroll">{children}</div>
      </div>
    </main>
  );
}
