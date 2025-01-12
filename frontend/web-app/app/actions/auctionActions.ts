'use server'

import { auth } from "@/auth";
import { Auction, PagedResult } from "../types";

export async function getData(query: string): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search${query}`);

    if (!res.ok) throw new Error('failed to fetch data');

    return res.json();
}

export async function updateAuctionTest() {
    const data = {
        name: "Name",
        author: "NewAuthor",
        year: 4000
    }

    const session = await auth();

    const res = await fetch("http://localhost:6001/auctions/afbee524-5972-4075-8800-7d1f9d7b0a0c", {
        method: "PUT",
        headers: {
            "Content-type": "application/json",
            "Authorization": "Bearer " + session?.accessToken, //this space is important as it ensures the token is not merged with the word
        },
        body: JSON.stringify(data),
    });

    if(!res.ok) return {status: res.status, message: res.statusText};

    return res.statusText;
}