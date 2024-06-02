'use client';
import { Button, Flex, Image, PinInput, Text } from '@mantine/core';
import { IconCircleCheck } from '@tabler/icons-react';
import Link from 'next/link';

import { useState } from 'react';

type VerifyPageProps = {
  params: {
    id: string;
  };
};

export default function VerifyPage({ params: { id } }: VerifyPageProps) {
  const [verified, setVerified] = useState(false);

  return (
    <Flex className="h-full flex-col items-center justify-between px-1 md:flex-row md:px-32">
      <Image src="/assets/welcome-image.png" w={600} h={600} alt="welcome" />
      {verified ? (
        <Flex className="w-full flex-col items-center justify-center px-0 md:px-20">
          <IconCircleCheck
            size="90px"
            className="text-primary-default"
            stroke={1}
          />
          <Text className="text-3xl" ta="center" mt="md" mb={15}>
            Successfully verified
          </Text>
          <Button mt={15} size="md" component={Link} href="/auth/sign-in">
            Login
          </Button>
        </Flex>
      ) : (
        <Flex className="w-full flex-col items-center justify-center px-0 md:px-20">
          <Text className="text-3xl" ta="center" mt="md" mb={15}>
            CHECK YOUR EMAIL
          </Text>
          <Text className="max-w-52 text-sm" c="dimmed" ta="center" mb={20}>
            We&apos;ve sent a verification code to your email address. Please
            enter the 4-digit code below to verify your account
          </Text>

          <PinInput size="md" />

          <Button mt={15} size="md" onClick={() => setVerified((p) => !p)}>
            Verify Code
          </Button>
        </Flex>
      )}
    </Flex>
  );
}
