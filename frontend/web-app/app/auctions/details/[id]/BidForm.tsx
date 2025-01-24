'use client'

import { placeBidForAuction } from "@/app/actions/auctionActions";
import { currencyFormatter } from "@/app/lib/utils";
import { useBidStore } from "@/hooks/useBidStore";
import React from "react";
import {FieldValues, useForm } from "react-hook-form";
import toast from "react-hot-toast";

type Props = {
    auctionId: string;
    currentHighestBid: number;

}

export default function BidForm({auctionId, currentHighestBid}: Props) {

    const {register, handleSubmit, reset} = useForm();
    const addBid = useBidStore(state => state.addBid);

    function onSubmit(data: FieldValues) {
        if(data.bidAmount <= currentHighestBid) {
            reset();
            return toast.error("Bid must be at least " + currencyFormatter.format(currentHighestBid + 1));
        }
        placeBidForAuction(auctionId, +data.bidAmount)
            .then(bid => {
                if(bid.error) throw bid.error;
                addBid(bid);
                reset();
            }).catch(err => toast.error(err.message));
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)}
            className="flex items-center border-2 rounded-lg py-2"
        >
           <input 
                type="number"
                {...register("bidAmount")}
                className="input-custom text-sm text-gray-600"
                placeholder={`Enter your bid (minimum bid is ${currencyFormatter.format(currentHighestBid + 1)} )`}
           /> 
        </form>
    )
}