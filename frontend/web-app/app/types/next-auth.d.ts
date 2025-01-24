import { type DefaultSession } from "next-auth"
 
declare module "next-auth" {
  interface Session {
    user: {
      username: string;
    } & DefaultSession["user"]
    accessToken: string;
  }
  interface Profile {
    username: string;
  }

  interface User {
    username: string;
  }
}

declare module "next-auth/jwt" {
    interface JWT {
        username: string;
        accessToken: string;
    }
}