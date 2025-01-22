import React from "react";
import {Auction, AuctionFinished } from "../types";
import Link from "next/link";
import Image from "next/image";

type Props = {
    finishedAuction: AuctionFinished
    auction: Auction
}

export default function AuctionFinishedToast({finishedAuction, auction} : Props) {
    return (
        <Link href={`/auctions/details/${auction.id}`}
            className="flex flex-col items-center" 
        >
            <div className="flex flex-row items-center gap-2">
                <Image 
                    src={auction.imageUrl}
                    alt="Image of book"
                    height={80}
                    width={80}
                    className="rounded-lg w-auto h-auto"
                />
                <div className="flex flex-col">
                    <span>
                        Auction for {auction.name} by {auction.author} has finished.
                    </span>
                    {finishedAuction.itemSold && finishedAuction.amount ? (
                        <p>Congrats to {finishedAuction.winner}!</p>
                    ) : (
                        <p>This item did not sell</p>
                    )}
                </div>
            </div>
        </Link>
    )
}