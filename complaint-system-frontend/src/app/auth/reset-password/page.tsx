'use client';
import { Button, Flex, Image, PasswordInput, Text } from '@mantine/core';
import { IconCircleCheck, IconPassword } from '@tabler/icons-react';
import Link from 'next/link';
import { useState } from 'react';

function ResetPassword() {
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
            Password Updated
          </Text>
          <Button mt={15} size="md" component={Link} href="/dashboard">
            Setup Your Profile
          </Button>
        </Flex>
      ) : (
        <Flex className="w-full flex-col justify-center px-0 md:px-20">
          <Text className="text-3xl" ta="center" mt="md" mb={10}>
            RESET YOUR PASSWORD
          </Text>
          <Text className="text-sm" ta="center" c="dimmed" mb={20}>
            Create a new password. Ensure it differs from previous ones for
            security
          </Text>

          <PasswordInput
            placeholder="Password"
            leftSection={<IconPassword size="20px" />}
            size="md"
            mb="md"
          />
          <PasswordInput
            placeholder="Confirm Password"
            leftSection={<IconPassword size="20px" />}
            size="md"
          />
          <Button mt="xl" onClick={() => setVerified((p) => !p)} size="md">
            Update Password
          </Button>
        </Flex>
      )}
    </Flex>
  );
}

export default ResetPassword;
