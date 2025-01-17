'use client'

import { useParamsStore } from "@/hooks/useParamsStore";
import { usePathname, useRouter } from "next/navigation";
import React, { useEffect, useState } from "react";
import { FaSearch } from "react-icons/fa";

export default function Search() {

    const router = useRouter();
    const pathname = usePathname();
    const searchTerm = useParamsStore(state => state.searchTerm);
    const setParams = useParamsStore(state => state.setParams);
    const [value, setValue] = useState("");

    useEffect(() => {
        setValue(searchTerm);
    }, [searchTerm])

    function onChange(event: React.ChangeEvent<HTMLInputElement>) {
        setValue(event.target.value)
    }
    
    function search() {
        const trimmedValue = value.trim();

        if(pathname !== "/") {
            router.push("/");
        }

        setParams({searchTerm: trimmedValue});
    }

    return (
        <div className="flex w-[50%] items-center border-2 rounded-full py-2 shadow-sm">
            <input 
                onKeyDown={(event: React.KeyboardEvent<HTMLInputElement>) => {
                    if (event.key === "Enter") search()
                }}
                value = {value}
                onChange={onChange}
                type="text"
                placeholder="Search for auctions by book name or author"
                className="flex-grow pl-5 bg-transparent focus:outline-none border-transparent focus:border-transparent
                    focus:ring-0 text-sm text-gray-600"
            />
            <button onClick={search}>
                <FaSearch size={34} className="bg-blue-500 text-white rounded-full p-2 cursor-pointer mx-2"/>
            </button>
        </div>
    )
}