# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80

# copy all .csproj and restore as distinct layers, use docker caching to speed up future builds
COPY BookBuy.sln BookBuy.sln

COPY src/AuctionService/AuctionService.csproj src/AuctionService/AuctionService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj

COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj

# restore package dependencies
RUN dotnet restore BookBuy.sln

# copy app folders
COPY src/GatewayService src/GatewayService
WORKDIR /app/src/GatewayService

RUN dotnet publish -c Release -o /app/src/out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "GatewayService.dll" ]