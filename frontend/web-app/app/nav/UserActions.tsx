
'use client'

import { useParamsStore } from "@/hooks/useParamsStore";
import { Dropdown, DropdownDivider, DropdownItem } from "flowbite-react";
import { User } from "next-auth";
import { signOut } from "next-auth/react";
import Link from "next/link";
import { usePathname, useRouter } from "next/navigation";
import React from "react";
import { AiFillBook, AiFillTrophy, AiOutlineLogout } from "react-icons/ai";
import { HiCog, HiUser } from "react-icons/hi2";

type Props = {
    user: User
}

export default function UserActions({user}: Props) {

    const router = useRouter();
    const pathname = usePathname();
    const setParams = useParamsStore(state => state.setParams);

    function setWinner(){
        setParams({winner: user.username, seller: undefined})

        if(pathname !== "/") router.push("/");
    }

    function setSeller(){
        setParams({winner: undefined, seller: user.username})

        if(pathname !== "/") router.push("/");
    }

    return (
        <Dropdown inline label={`Welcome ${user.name}`}>
            <DropdownItem icon={HiUser} onClick={setSeller}>
                My Auctions
            </DropdownItem>
            <DropdownItem icon={AiFillTrophy} onClick={setWinner}>
                Auctions won
            </DropdownItem>
            <DropdownItem icon={AiFillBook}>
                <Link href="/auctions/create">
                    Sell my book
                </Link>
            </DropdownItem>
            <DropdownItem icon={HiCog}>
                <Link href="/session">
                    Session
                </Link>
            </DropdownItem>
            <DropdownDivider  />
            <DropdownItem icon={AiOutlineLogout} onClick={() => signOut({redirectTo: "/"})}>
                Sign out
            </DropdownItem>
        </Dropdown>
    )
}