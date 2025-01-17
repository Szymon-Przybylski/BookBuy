import Heading from "@/app/components/Heading";
import React from "react";
import AuctionForm from "../../AuctionForm";
import { getDetailedViewData } from "@/app/actions/auctionActions";

export default async function Update({params}: {params: {id: string}}) {
    const resolvedParams = await Promise.resolve(params);

    const data = await getDetailedViewData(resolvedParams.id)

    return (
        <div className="mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg">
            <Heading title="Update your auction" subtitle="Please update the details of your book" />
            <AuctionForm auction={data} />
        </div>
    )
}