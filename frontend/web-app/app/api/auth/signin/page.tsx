import EmptyFilter from "@/app/components/EmptyFilter";
import React from "react";

export default async function SignIn(props: { params: Promise<{ callbackUrl: string }> }) {
  const params = await props.params;
  return (
    <EmptyFilter
      title="You need to be logged in to access this page"
      subtitle="Please click below to log in"
      showLogin
      redirectToUrl={params.callbackUrl}
    />
  );
}