import { Box } from '@mantine/core';
import React from 'react';

export default async function AuthLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <Box className="h-screen min-h-[600px]  p-4">
      <Box className="w-full items-center justify-center">{children}</Box>
    </Box>
  );
}
