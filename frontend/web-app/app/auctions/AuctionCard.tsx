import React from 'react'
import CountdownTimer from './CountdownTimer';
import BookImage from './BookImage';
import { Auction } from '../types';

type Props = {
    auction: Auction
}

export default function AuctionCard({auction}: Props) {

    return (
        <a href='#' className='group'>
            <div className=' relative w-full bg-gray-200 aspect-[16/10] rounded-lg overflow-hidden'>
                <BookImage imageUrl={auction.imageUrl} name={auction.name} author={auction.author} />
                <div className='absolute bottom-2 left-2'>
                    <CountdownTimer auctionEndingAt={auction.auctionEndingAt} />
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
        </a>
    )
}