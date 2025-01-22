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

export type Bid = {
    id: string
    auctionId: string
    bidder: string
    bidDate: string
    bidAmount: number
    bidStatus: string
}

export type AuctionFinished = {
    itemSold: boolean
    auctionId: string
    winner?: string
    seller: string
    amount?: number
}