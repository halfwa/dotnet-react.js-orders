#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/OrderCreator.API/OrderCreator.API.csproj", "backend/OrderCreator.API/"]
COPY ["backend/OrderCreator.Application/OrderCreator.Application.csproj", "backend/OrderCreator.Application/"]
COPY ["backend/OrderCreator.Core/OrderCreator.Core.csproj", "backend/OrderCreator.Core/"]
COPY ["backend/OrderCreator.DataAccess/OrderCreator.DataAccess.csproj", "backend/OrderCreator.DataAccess/"]
RUN dotnet restore "./backend/OrderCreator.API/./OrderCreator.API.csproj"
COPY . .
WORKDIR "/src/backend/OrderCreator.API"
RUN dotnet build "./OrderCreator.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderCreator.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final	
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderCreator.API.dll"]