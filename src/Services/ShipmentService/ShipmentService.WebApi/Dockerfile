#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/ShipmentService/ShipmentService.WebApi/ShipmentService.WebApi.csproj", "src/Services/ShipmentService/ShipmentService.WebApi/"]
COPY ["src/Services/ShipmentService/ShipmentService.Infrastructure/ShipmentService.Infrastructure.csproj", "src/Services/ShipmentService/ShipmentService.Infrastructure/"]
COPY ["src/Services/OrderService/OrderService.IntegrationEvents/OrderService.IntegrationEvents.csproj", "src/Services/OrderService/OrderService.IntegrationEvents/"]
COPY ["src/Services/ShipmentService/ShipmentService.Aplication/ShipmentService.Aplication.csproj", "src/Services/ShipmentService/ShipmentService.Aplication/"]
COPY ["src/Services/ShipmentService/ShipmentService.Domain/ShipmentService.Domain.csproj", "src/Services/ShipmentService/ShipmentService.Domain/"]
COPY ["src/Services/ShipmentService/ShipmentService.IntegrationEvent/ShipmentService.IntegrationEvent.csproj", "src/Services/ShipmentService/ShipmentService.IntegrationEvent/"]
RUN dotnet restore "./src/Services/ShipmentService/ShipmentService.WebApi/ShipmentService.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Services/ShipmentService/ShipmentService.WebApi"
RUN dotnet build "./ShipmentService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShipmentService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipmentService.WebApi.dll"]