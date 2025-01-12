'use client'

import { useParamsStore } from "@/hooks/useParamsStore";
import React from "react";
import Heading from "./Heading";
import { Button } from "flowbite-react";
import { signIn } from "next-auth/react";

type Props = {
    title?: string,
    subtitle?: string,
    showReset?: boolean,
    showLogin?: boolean,
    redirectToUrl?: string,
}

export default function EmptyFilter({
    title = "No matches for this filter",
    subtitle = "Try changing filters or resetting search field",
    showReset,
    showLogin,
    redirectToUrl
} : Props) {

    const reset = useParamsStore(state => state.reset);

    return (
        <div className="h-[40vh] flex flex-col gap-2 justify-center items-center shadow-lg p-4">
            <Heading title={title} subtitle={subtitle} center />
            <div className="mt-4">
                {showReset && (
                    <Button outline onClick={reset}>
                        Remove filters
                    </Button>
                )}
                {showLogin && (
                    <Button outline onClick={() => signIn("id-server", {redirectTo: redirectToUrl})}>
                        Login
                    </Button>
                )}
            </div>
        </div>
    )
}