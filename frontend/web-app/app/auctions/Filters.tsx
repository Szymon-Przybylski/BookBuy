import { Button, ButtonGroup } from "flowbite-react";
import React from "react";

type Props = {
    pageSize: number;
    setPageSize: (size: number) => void;
}

const pageSizeButtons = [2,4,8,12];

export default function Filters({pageSize, setPageSize} : Props) {
    return (
        <div className="flex justify-between items-center mb-4">
            <div>
                <span className="uppercase text-sm text-gray-500">Page size</span>
                <ButtonGroup>
                    {pageSizeButtons.map((value, i) => (
                        <Button key={i} onClick={() => setPageSize(value)} 
                        color={`${pageSize === value ? "red" : "gray"}`}
                        className="focus:ring-0"
                        >
                            {value}
                        </Button>
                    ))}
                </ButtonGroup>
            </div>
        </div>
    )
}