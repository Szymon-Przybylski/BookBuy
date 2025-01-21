import React from "react";

type Props = {
    bidAmount?: number,
    reservePrice: number,
}

export default function CurrentBid({bidAmount, reservePrice}: Props) {

    const text = bidAmount ? "$" + bidAmount : "No bids";
    const color = bidAmount ? bidAmount > reservePrice ? "bg-green-600" : "bg-amber-600" : "bg-red-600";

    return (
        <div className={`
            border-2 border-white text-white py-1 px-2 rounded-lg flex
            justify-center ${color}
        `}>
            {text}
        </div>
    )
}