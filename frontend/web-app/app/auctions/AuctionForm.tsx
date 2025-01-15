'use client'

import { Button, TextInput } from "flowbite-react";
import React, { useEffect } from "react";
import { FieldValues, useForm } from "react-hook-form";
import Input from "../components/Input";

export default function AuctionForm() {

    const {control, handleSubmit, setFocus,
        formState: {isSubmitting, isValid, isDirty, errors}} = useForm({
           mode: "onTouched"
        });

    useEffect(() => {
        setFocus("name");
    }, [setFocus]);

    function onSubmit(data: FieldValues){
        console.log(data);
    }

    return (
        <form className="flex flex-col mt-3" onSubmit={handleSubmit(onSubmit)}>

            <Input label="Name" name="name" control={control} rules={{required: "Name is required"}}/>
            <Input label="Author" name="author" control={control} rules={{required: "Author is required"}}/>

            <div className="grid grid-cols-8 gap-3">
                <Input label="Year" name="year" control={control} type="number" rules={{required: "Year is required"}}/>
            </div>

            <Input label="Image URL" name="imageUrl" control={control} rules={{required: "Image URL is required"}}/>

            <div className="grid grid-cols-2 gap-3">
                <Input label="Reserve Price (enter 0 if no reserve)" name="reservePrice"
                    control={control} type="number" rules={{required: "Reserve price is required"}}/>
                <Input label="Auction ending at" name="autcionEndingAt" control={control} 
                    type="date" rules={{required: "Auction end date is required"}}/>
            </div>

            <div className="flex justify-between">
                <Button outline color="gray">
                    Cancel
                </Button>
                <Button isProcessing={isSubmitting} type="submit" outline color="success">
                    Submit
                </Button>
            </div>
        </form>
    )
}