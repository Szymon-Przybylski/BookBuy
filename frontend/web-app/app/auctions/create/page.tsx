import Heading from "@/app/components/Heading";
import React from "react";
import AuctionForm from "../AuctionForm";

export default function Create() {
    return (
        <div className="mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg">
            <Heading title="Sell your book!" subtitle="Please enter the details in the form below"/>
            <AuctionForm/>
        </div>
    )
}