#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/OrderService/OrderServiceWebAPI/OrderServiceWebAPI.csproj", "src/Services/OrderService/OrderServiceWebAPI/"]
COPY ["src/Services/OrderService/OrderService.Infrastructure/OrderService.Infrastructure.csproj", "src/Services/OrderService/OrderService.Infrastructure/"]
COPY ["src/Services/PaymentService/PaymentService.IntegrationEvents/PaymentService.IntegrationEvents.csproj", "src/Services/PaymentService/PaymentService.IntegrationEvents/"]
COPY ["src/Services/ShipmentService/ShipmentService.IntegrationEvent/ShipmentService.IntegrationEvent.csproj", "src/Services/ShipmentService/ShipmentService.IntegrationEvent/"]
COPY ["src/Services/OrderService/OrderService.Application/OrderService.Application.csproj", "src/Services/OrderService/OrderService.Application/"]
COPY ["src/Services/OrderService/OrderService.Domain/OrderService.Domain.csproj", "src/Services/OrderService/OrderService.Domain/"]
COPY ["src/Services/OrderService/OrderService.IntegrationEvents/OrderService.IntegrationEvents.csproj", "src/Services/OrderService/OrderService.IntegrationEvents/"]
RUN dotnet restore "./src/Services/OrderService/OrderServiceWebAPI/OrderServiceWebAPI.csproj"
COPY . .
WORKDIR "/src/src/Services/OrderService/OrderServiceWebAPI"
RUN dotnet build "./OrderServiceWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderServiceWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderServiceWebAPI.dll"]