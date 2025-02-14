'use client'

import React, { useState } from "react";
import { updateAuctionTest } from "../actions/auctionActions";
import { Button } from "flowbite-react";

interface AuctionUpdateResponse {
    success: boolean;
    message: string;
    data?: {
        id: string;
        name: string;
        author: string;
        year: number;
    };
}

export default function AuthTest() {

    const [loading, setLoading] = useState(false);
    const [result, setResult] = useState<AuctionUpdateResponse | null>(null);

    function doUpdate() {
        setResult(null);
        setLoading(true);
        updateAuctionTest()
        .then(res => setResult(res))
        .catch(err => setResult(err))
        .finally(() => setLoading(false));
    }

    return (
        <div className="flex items-center gap-4">
            <Button outline isProcessing={loading} onClick={doUpdate}>
                Test auth
            </Button>
            <div>
                {result !== null && JSON.stringify(result, null, 2)}
            </div>
        </div>
    )
}