import { Button, Flex, Image, Text, TextInput } from '@mantine/core';
import { IconMail } from '@tabler/icons-react';
import Link from 'next/link';

function Reset() {
  return (
    <Flex className="h-full flex-col items-center justify-between px-1 md:flex-row md:px-32">
      <Image src="/assets/welcome-image.png" w={600} h={600} alt="welcome" />
      <Flex className="w-full flex-col justify-center px-0 md:px-20">
        <Text className="text-3xl" ta="center" mt="md" mb={10}>
          FORGOT PASSWORD
        </Text>
        <Text className="text-sm" ta="center" c="dimmed" mb={20}>
          Please enter your email to reset the password
        </Text>

        <TextInput
          placeholder="Your Email"
          leftSection={<IconMail size="20px" />}
          size="md"
        />
        <Link href="/auth/verify/1" className="mx-auto">
          <Button mt="xl" size="md">
            Reset Password
          </Button>
        </Link>
      </Flex>
    </Flex>
  );
}

export default Reset;
