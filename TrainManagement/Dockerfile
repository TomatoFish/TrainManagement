﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TrainManagement/TrainManagement.csproj", "TrainManagement/"]
RUN dotnet restore "TrainManagement/TrainManagement.csproj"
COPY . .
WORKDIR "/src/TrainManagement"
RUN dotnet build "TrainManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrainManagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrainManagement.dll"]
