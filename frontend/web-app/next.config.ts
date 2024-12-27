import type { NextConfig } from "next";

const nextConfig: NextConfig = {
    logging: {
        fetches: {
            fullUrl: true
        }
    },
    images: {
        remotePatterns: [
            {
                protocol: 'https',
                hostname: 'cdn.prod.website-files.com'
            },
            {
                protocol: 'https',
                hostname: 'cdn.pixabay.com'
            }
        ]
    }
};

export default nextConfig;
