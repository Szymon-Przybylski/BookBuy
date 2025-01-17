'use client'

import { Button } from "flowbite-react";
import React, { useEffect } from "react";
import { FieldValues, useForm } from "react-hook-form";
import Input from "../components/Input";
import DateInput from "../components/DateInput";
import { createAuction, updateAuction } from "../actions/auctionActions";
import { usePathname, useRouter } from "next/navigation";
import toast from "react-hot-toast";
import { Auction } from "../types";

type Props = {
    auction?: Auction
}

export default function AuctionForm({auction} : Props) {

    const router = useRouter();
    const pathname = usePathname();
    const {control, handleSubmit, setFocus, reset, setValue,
        formState: {isSubmitting, isValid}} = useForm({
           mode: "onTouched",
        })

    useEffect(() => {

        setValue("imageUrl", "https://cdn.prod.website-files.com/65469043b68c5cc835bcbbc5/6579b68d8a290d78c8b89d22_cover_splash_sm.jpg");
        if(auction) {
            const {name, author, year} = auction;
            reset({name, author, year});
        }
        setFocus("name");
    }, [auction, reset, setFocus, setValue]);

    async function onSubmit(data: FieldValues){

        try {
            let id = "";
            let res;

            if(pathname === "/auctions/create") {
                res = await createAuction(data);
                id = res.id;
            } else {
                if (auction) {
                    res = await updateAuction(data, auction.id);
                    id = auction.id;
                }
            }

            if(res.error) {
                throw res.error;
            }
            router.push(`/auctions/details/${id}`);

        } catch (error: any) {
            toast.error(error.status + " " + error.message);
        }
    }

    const onCancel = () => {
        if (pathname === '/auctions/create') {
          router.push('/');
        } else {
          router.push(`/auctions/details/${auction?.id}`);
        }
      };

    return (
        <form className="flex flex-col mt-3" onSubmit={handleSubmit(onSubmit)}>

            <Input label="Name" name="name" control={control} rules={{required: "Name is required"}}/>
            <Input label="Author" name="author" control={control} rules={{required: "Author is required"}}/>

            <div className="grid grid-cols-8 gap-3">
                <Input label="Year" name="year" control={control} type="number" rules={{required: "Year is required"}}/>
            </div>

            {pathname === "/auctions/create" &&
                <>
                    <Input label="Image URL" name="imageUrl" control={control} rules={{required: "Image URL is required"}}/>

                    <div className="grid grid-cols-2 gap-3">
                        <Input label="Reserve Price (enter 0 if no reserve)" name="reservePrice"
                            control={control} type="number" rules={{required: "Reserve price is required"}}/>
                        <DateInput label="Auction ending at" name="auctionEndingAt" control={control} 
                            dateFormat="dd MMMM yyyy h:mm a" rules={{required: "Auction end date is required"}}
                            showTimeSelect
                            />
                    </div>
                </>
            }

            <div className="flex justify-between">
                <Button outline color="gray" onClick={onCancel}>
                    Cancel
                </Button>
                <Button isProcessing={isSubmitting} disabled={!isValid} type="submit" outline color="success">
                    Submit
                </Button>
            </div>
        </form>
    );
}