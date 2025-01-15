import React from "react";

export default async function Details({ params }: { params: { id: string } }) {
    const resolvedParams = await Promise.resolve(params);

    return (
        <div>Details of {resolvedParams.id}</div>
    );
}