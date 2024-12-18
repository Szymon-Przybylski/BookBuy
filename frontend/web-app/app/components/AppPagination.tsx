'use client'

import { Pagination } from 'flowbite-react';
import React from 'react';

type Props = {
    currentPage: number
    pagecount: number
    pageChanged: (page: number) => void
}

export default function AppPagination({currentPage, pagecount, pageChanged}: Props) {

    return (
        <Pagination 
            currentPage={currentPage} 
            onPageChange={e => pageChanged(e)} 
            totalPages={pagecount} 
            layout='pagination'
            showIcons={true}
            className='text-blue-500 mb-5'
            />
    )
}