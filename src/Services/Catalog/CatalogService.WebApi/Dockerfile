#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/CatalogService.WebApi/CatalogService.WebApi.csproj", "src/Services/Catalog/CatalogService.WebApi/"]
COPY ["src/Services/Catalog/CatalogService.Application/CatalogService.Application.csproj", "src/Services/Catalog/CatalogService.Application/"]
COPY ["src/Services/Catalog/CatalogService.Contracts/CatalogService.Contracts.csproj", "src/Services/Catalog/CatalogService.Contracts/"]
COPY ["src/Services/Catalog/CatalogService.Domain/CatalogService.Domain.csproj", "src/Services/Catalog/CatalogService.Domain/"]
COPY ["src/Services/Catalog/CatalogService.Infostructure/CatalogService.Infostructure.csproj", "src/Services/Catalog/CatalogService.Infostructure/"]
COPY ["src/Services/IdentityService/IdentityService.IntegrationEvents/IdentityService.IntegrationEvents.csproj", "src/Services/IdentityService/IdentityService.IntegrationEvents/"]
RUN dotnet restore "./src/Services/Catalog/CatalogService.WebApi/CatalogService.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/CatalogService.WebApi"
RUN dotnet build "./CatalogService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build
   
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CatalogService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogService.WebApi.dll"]