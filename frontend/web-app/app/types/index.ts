export type PagedResult<T> = {
    result: T[]
    pageCount: number
    totalCount: number
}

export type Auction = {
    id: string
    reservePrice: number
    seller: string
    winner?: string
    soldAmount: number
    currentHighestBid: number
    myProperty: number
    auctionCreatedAt: string
    auctionUpdatedAt: string
    auctionEndingAt: string
    status: string
    name: string
    author: string
    year: number
    imageUrl: string
}
