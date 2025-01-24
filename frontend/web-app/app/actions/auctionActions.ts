'use server'

import { FieldValues } from "react-hook-form";
import { Auction, Bid, PagedResult } from "../types";
import { fetchWrapper } from "@/app/lib/fetchWrapper";
import { revalidatePath } from "next/cache";

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

export async function getData(query: string): Promise<PagedResult<Auction>> {
    
    return await fetchWrapper.get(`search${query}`);

}

export async function createAuction(data: FieldValues) {
    return await fetchWrapper.post("auctions", data);
}

export async function getDetailedViewData(id: string): Promise<Auction> {
    return await fetchWrapper.get(`auctions/${id}`);
}

export async function updateAuction(data: FieldValues, id: string) {
    const res = await fetchWrapper.put(`auctions/${id}`, data);
    revalidatePath(`/auctions/${id}`);
    return res;
}

export async function deleteAuction(id: string) {
    return await fetchWrapper.del(`auctions/${id}`);
}

export async function updateAuctionTest(): Promise<AuctionUpdateResponse> {
    const data = {
        name: "Name",
        author: "NewAuthor",
        year: 4000
    }

    return await fetchWrapper.put("auctions/afbee524-5972-4075-8800-7d1f9d7b0a0c", data);
}

export async function getBidsForAuction(id: string): Promise<Bid[]> {
    return await fetchWrapper.get(`bids/${id}`);
}

export async function placeBidForAuction(auctionId: string, bidAmount: number) {
    return await fetchWrapper.post(`bids?auctionId=${auctionId}&bidAmount=${bidAmount}`, {})
}