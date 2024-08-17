#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/CartService/CartService.Api/CartService.Api.csproj", "src/Services/CartService/CartService.Api/"]
COPY ["src/Services/CartService/CartService.Aplication/CartService.Aplication.csproj", "src/Services/CartService/CartService.Aplication/"]
COPY ["src/Services/CartService/CartService.Domain/CartService.Domain.csproj", "src/Services/CartService/CartService.Domain/"]
COPY ["src/Services/CartService/CartService.Consumers/CartService.Consumers.csproj", "src/Services/CartService/CartService.Consumers/"]
COPY ["src/Services/Catalog/CatalogService.Contracts/CatalogService.Contracts.csproj", "src/Services/Catalog/CatalogService.Contracts/"]
COPY ["src/Services/CartService/CartService.Infrastructure/CartService.Infrastructure.csproj", "src/Services/CartService/CartService.Infrastructure/"]
RUN dotnet restore "./src/Services/CartService/CartService.Api/CartService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/CartService/CartService.Api"
RUN dotnet build "./CartService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CartService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartService.Api.dll"]