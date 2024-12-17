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
            }
        ]
    }
};

export default nextConfig;
