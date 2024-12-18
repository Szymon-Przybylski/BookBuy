'use server'

import { Auction, PagedResult } from "../types";

export async function getData(pageNumber: number = 1, pageSize: number): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search?pageSize=${pageSize}&pageNumber=${pageNumber}`);

    if (!res.ok) throw new Error('failed to fetch data');

    return res.json();
}