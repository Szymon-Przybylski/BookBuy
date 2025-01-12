
'use client'

import { Dropdown, DropdownDivider, DropdownItem } from "flowbite-react";
import { User } from "next-auth";
import { signOut } from "next-auth/react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import React from "react";
import { AiFillBook, AiFillTrophy, AiOutlineLogout } from "react-icons/ai";
import { HiCog, HiUser } from "react-icons/hi2";

type Props = {
    user: User
}

export default function UserActions({user}: Props) {

    const router = useRouter();

    return (
        <Dropdown inline label={`Welcome ${user.name}`}>
            <DropdownItem icon={HiUser}>
                <Link href="/">
                    My Auctions
                </Link>
            </DropdownItem>
            <DropdownItem icon={AiFillTrophy}>
                <Link href="/">
                    Auctions won
                </Link>
            </DropdownItem>
            <DropdownItem icon={AiFillBook}>
                <Link href="/">
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