'use client'

import React, {useState} from 'react'
import Image from 'next/image'

type Props = {
    imageUrl: string
    name: string
    author: string
}

export default function BookImage({ imageUrl, name, author }: Props) {

    const [isLoading, setLoading] = useState(true)

    return (
        <div className="relative w-full h-0" style={{ paddingBottom: '75%' }}>
            <Image
                src={imageUrl}
                alt={`Image of ${name} by ${author}`}
                fill
                priority
                className={`
                    group-hover:opacity-75 duration-700 ease-in-out object-cover object-center
                    ${isLoading ? 'grayscale blur-2xl scale-110' : 'grayscale-0 blur-0 scale-100'}
                `}
                sizes='(max-width: 768px) 100vw, (max-width: 1200px) 50vw, 25vw'
                onLoad={() => setLoading(false) }
            />
        </div>
    )
}