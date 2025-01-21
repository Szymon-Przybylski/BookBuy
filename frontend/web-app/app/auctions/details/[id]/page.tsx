import { getBidsForAuction, getDetailedViewData } from "@/app/actions/auctionActions";
import Heading from "@/app/components/Heading";
import React from "react";
import CountdownTimer from "../../CountdownTimer";
import BookImage from "../../BookImage";
import DetailedSpecs from "./DetailedSpecs";
import { getCurrentUser } from "@/app/actions/authActions";
import EditButton from "./EditButton";
import DeleteButton from "./DeleteButton";
import BidItem from "./BidItem";

export default async function Details({ params }: { params: { id: string } }) {
    const resolvedParams = await Promise.resolve(params);

    const data = await getDetailedViewData(resolvedParams.id);
    const user = await getCurrentUser();
    const bids = await getBidsForAuction(resolvedParams.id);

    return (
        <div>
            <div className="flex justify-between">
                <div className="flex items-center gap-3">
                    <Heading title={`${data.name} by ${data.author}`} subtitle=""/>
                    {user?.username === data.seller && (
                        <>
                            <EditButton id={data.id}/>
                            <DeleteButton id={data.id} />
                        </>
                    ) }
                </div>
                <div className="flex gap-3">
                    <h3 className="text-2xl font-semibold">
                        Time remaining: 
                    </h3>
                    <CountdownTimer auctionEndingAt={data.auctionEndingAt}/>
                </div>
            </div>
            <div className="grid grid-cols-2 gap-6 mt-3 items-start">
                <div className="max-w-sm w-full bg-gray-200 rounded-lg overflow-hidden">
                    <BookImage imageUrl={data.imageUrl} name={data.name} author={data.author}/>
                </div>
                <div className="border-2 rounded-lg p-2 bg-gray-100 flex flex-col h-full">
                    <Heading title="Bids" subtitle=""/>
                    {bids.map(bid => (
                        <BidItem key={bid.id} bid={bid}/>
                    ))}
                </div>
            </div>
            <div className="mt-3 grid grid-cols-1 rounded-lg">
                <DetailedSpecs  auction={data}/>
            </div>
        </div>
    );
}