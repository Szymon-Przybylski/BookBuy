import React from 'react'
import { FaBook } from 'react-icons/fa'
export default function Navbar() {
    return (
        <header className='
            sticky top-0 z-50 flex justify-between bg-white
            p-5 item-center text-grey-800 shadow-md'>
            <div className='flex items-center gap-2 text-3xl font-semibold text-blue-500'>
                <FaBook size={34} />
                <div>BookBuy Auctions</div>
            </div>
            <div>Search</div>
            <div>Login</div>
        </header>
    )
}