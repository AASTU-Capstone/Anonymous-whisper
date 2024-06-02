import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";
import { cookies } from "next/headers";

export function middleware(request: NextRequest) {
  const cookieStore = cookies();
  const token = cookieStore.get("token");
  const path = request.nextUrl.pathname;
  const referer = request.headers.get("referer");

  // Protect /app/settings route
  if (!token?.value && request.nextUrl.pathname.startsWith("/app/settings")) {
    return NextResponse.redirect(new URL("/auth/login", request.url));
  }

  // Protect /auth/login if the user is already authenticated
  if (token?.value && request.nextUrl.pathname.startsWith("/auth/login")) {
    return NextResponse.redirect(new URL("/app", request.url));
  }
  // Protect /auth/signup if the user is already authenticated
  if (token?.value && request.nextUrl.pathname.startsWith("/auth/signup")) {
    return NextResponse.redirect(new URL("/app", request.url));
  }

  // Protect /auth/create-profile/ route if a user is logged in
  if (
    token?.value &&
    request.nextUrl.pathname.startsWith("/auth/create-profile")
  ) {
    return NextResponse.redirect(new URL("/app", request.url));
  }

  // Protect /auth/signup/verify-otp route
  if (path === "/auth/signup/verify-otp") {
    // allow the user if they are coming from the signup route
    if (referer && referer.includes("/auth/signup")) {
      return NextResponse.next();
    } else {
      // deny the user if they are not coming from the signup route
      return NextResponse.redirect(new URL("/auth/signup", request.url));
    }
  }

  // Protect /auth/success route
  if (path === "/auth/success") {
    // allow the user if they come from the verifiy-otp route
    if (referer && referer.includes("/auth/signup/verify-otp")) {
      return NextResponse.next();
    } else {
      // unauthorized , the user will be redirected to the app
      return NextResponse.redirect(new URL("/app", request.url));
    }
  }

  // Protect /auth/reset-password/change route
  if (path === "/auth/reset-password/change") {
    // allow the user if they come from the reset-password route
    if (referer && referer.includes("/auth/reset-password/verify-otp")) {
      return NextResponse.next();
    } else {
      // unauthorized , the user will be redirected to the app
      return NextResponse.redirect(new URL("/app", request.url));
    }
  }

  // Protect /auth/password-updated route
  if (path === "/auth/password-updated") {
    // allow the user if they come from the reset-password route
    if (referer && referer.includes("/auth/reset-password/change")) {
      return NextResponse.next();
    } else {
      // unauthorized , the user will be redirected to the app
      return NextResponse.redirect(new URL("/app", request.url));
    }
  }

  // Protect /auth/reset-password/verify-otp route
  if (path === "/auth/reset-password/verify-otp") {
    // allow the user if they come from the reset-password route
    if (referer && referer.includes("/auth/reset-password")) {
      return NextResponse.next();
    } else {
      // unauthorized , the user will be redirected to the app
      return NextResponse.redirect(
        new URL("/auth/reset-password", request.url)
      );
    }
  }

  return NextResponse.next();
}

{
  /*

the routes that are not protected are the following:
- /auth/create-profile => should it be implemented? .... not sure
- /auth/reset-password => because the ui is not done yet

the routes that are protected are the following:
- /auth/signup/verify-otp
- /auth/success
- /auth/reset-password/verify-otp
- /auth/password-updated
- /auth/reset-password/change

Any Route that starts with /settings is protected and should be accessed only if the user is authenticated and is logged in.

*/
}
