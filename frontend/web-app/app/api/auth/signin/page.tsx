import EmptyFilter from "@/app/components/EmptyFilter";
import React from "react";

export default function SignIn({searchParams} :  {searchParams: {callbackUrl: string}}) {
    return (
        <EmptyFilter
            title="You need to be logged in to access this page"
            subtitle="Please click below to log in"
            showLogin
            redirectToUrl={searchParams.callbackUrl}
        />
    )
}