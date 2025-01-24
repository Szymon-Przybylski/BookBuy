import EmptyFilter from "@/app/components/EmptyFilter";
import React from "react";

export default async function SignIn({ params }: { params: { callbackUrl: string } }) {
  return (
    <EmptyFilter
      title="You need to be logged in to access this page"
      subtitle="Please click below to log in"
      showLogin
      redirectToUrl={params.callbackUrl}
    />
  );
}