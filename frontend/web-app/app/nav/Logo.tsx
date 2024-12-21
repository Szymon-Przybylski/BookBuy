'use client'

import { useParamsStore } from "@/hooks/useParamsStore";
import React from "react"
import { FaBook } from "react-icons/fa"

export default function Logo() {

    const reset = useParamsStore(state => state.reset);

    return (
        <div onClick={reset}
            className='cursor-point flex items-center gap-2 text-3xl font-semibold text-blue-500'>
                <FaBook size={34} />
                <div>BookBuy Auctions</div>
        </div>
    )
}