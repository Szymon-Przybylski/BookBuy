import React from 'react'
import CountdownTimer from './CountdownTimer';
import BookImage from './BookImage';
import { Auction } from '../types';
import Link from 'next/link';
import CurrentBid from './CurrentBid';

type Props = {
    auction: Auction
}

export default function AuctionCard({auction}: Props) {

    return (
        <Link href={`/auctions/details/${auction.id}`} className='group'>
            <div className=' relative w-full bg-gray-200 aspect-[16/10] rounded-lg overflow-hidden'>
                <BookImage imageUrl={auction.imageUrl} name={auction.name} author={auction.author} />
                <div className='absolute bottom-2 left-2'>
                    <CountdownTimer auctionEndingAt={auction.auctionEndingAt} />
                </div>
                <div className='absolute top-2 right-2'>
                    <CurrentBid reservePrice={auction.reservePrice} bidAmount={auction.currentHighestBid} />
                </div>
            </div>
            <div className='flex justify-between items-center mt-4'>
                <h3 className='text=gray-700'>
                    {auction.name} {auction.author }
                </h3>
                <p className='font-semibold text-sm'>
                    {auction.year }
                </p>
            </div>
        </Link>
    )
}