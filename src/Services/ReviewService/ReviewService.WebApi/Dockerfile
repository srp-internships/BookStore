#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/ReviewService/ReviewService.WebApi/ReviewService.WebApi.csproj", "src/Services/ReviewService/ReviewService.WebApi/"]
COPY ["src/Services/ReviewService/ReviewService.Infrastructure/ReviewService.Infrastructure.csproj", "src/Services/ReviewService/ReviewService.Infrastructure/"]
COPY ["src/Services/Catalog/CatalogService.Contracts/CatalogService.Contracts.csproj", "src/Services/Catalog/CatalogService.Contracts/"]
COPY ["src/Services/ReviewService/ReviewService.Application/ReviewService.Application.csproj", "src/Services/ReviewService/ReviewService.Application/"]
COPY ["src/Services/ReviewService/ReviewService.Domain/ReviewService.Domain.csproj", "src/Services/ReviewService/ReviewService.Domain/"]
RUN dotnet restore "./src/Services/ReviewService/ReviewService.WebApi/ReviewService.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Services/ReviewService/ReviewService.WebApi"
RUN dotnet build "./ReviewService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ReviewService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReviewService.WebApi.dll"]