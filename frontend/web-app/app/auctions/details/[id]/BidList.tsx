'use client'

import { getBidsForAuction } from "@/app/actions/auctionActions";
import { Auction, Bid } from "@/app/types";
import { useBidStore } from "@/hooks/useBidStore";
import { User } from "next-auth";
import React, { useEffect, useState } from "react";
import toast from "react-hot-toast";
import Heading from "@/app/components/Heading";
import BidItem from "./BidItem";
import { currencyFormatter } from "@/app/lib/utils";
import EmptyFilter from "@/app/components/EmptyFilter";
import BidForm from "./BidForm";

type Props = {
    user: User | null
    auction: Auction
}

export default function BidList({user, auction}: Props) {

    const [loading, setLoading] = useState(true);

    const bids = useBidStore(state => state.bids);
    const setBids = useBidStore(state => state.setBids);

    const open = useBidStore(state => state.open);
    const setOpen = useBidStore(state => state.setOpen);
    const openForBids = new Date(auction.auctionEndingAt) > new Date;

    const currentHighestBid = bids.reduce((prev, current) =>
         prev > current.bidAmount 
        ? prev 
        : current.bidStatus.includes("Accepted") 
        ? current.bidAmount 
        : prev, 0);

    useEffect(() => {
        setOpen(openForBids);
        getBidsForAuction(auction.id)
            .then((res: Bid[]) => {
                setBids(res as Bid[]);
            }).catch((err: Error) => {
                toast.error(err.message);
            }).finally(() => setLoading(false))
    }, [auction.id, openForBids, setBids, setLoading, setOpen]);

    if (loading) {
        return <span>Loading bids...</span>
    }
    
    return (
        <div className="border-2 rounded-lg shadow-md p-2 bg-gray-100 flex flex-col h-full">
            <div className="py-2 px-4 bg-white">
                <div className="sticky top-0 bg-white p-2">
                    <Heading title={`Current highest bid is ${currencyFormatter.format(currentHighestBid)}`} subtitle=""/>
                </div>
            </div>

            <div className="overflow-auto h-[400px] flex flex-col-reverse px-2">
                {bids.length === 0 ? (
                    <EmptyFilter title="No bids for this item" subtitle="Please feel free to make a bid"/>
                ) : (
                    <>
                    {bids
                        .filter((x) => x.auctionId === auction.id)
                        .map((bid) => (
                    <BidItem key={bid.id} bid={bid} />
              ))}
                    </>
                )}
            </div>
            <div className="px-2 pb-2 text-gray-500">
                {!open ? (
                    <div className="flex items-center justify-center p-2 text-lg font-semibold">
                        This auction has finished.
                    </div>
                ): !user ? (
                    <div className="flex items-center justify-center p-2 text-lg font-semibold">
                        Please log in to make a bid.
                    </div>
                ) : user && user.username === auction.seller ? (
                    <div className="flex items-center justify-center p-2 text-lg font-semibold">
                        You cannot bid on your own auction.
                    </div>
                ) : (
                    <BidForm auctionId={auction.id} currentHighestBid={currentHighestBid} />
                )}
            </div>
        </div>
    )
}