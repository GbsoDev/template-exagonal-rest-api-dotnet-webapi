#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GbsoDevExagonalTemplate.Api/GbsoDevExagonalTemplate.Api.csproj", "GbsoDevExagonalTemplate.Api/"]
COPY ["GbsoDevExagonalTemplate.Domain/GbsoDevExagonalTemplate.Domain.csproj", "GbsoDevExagonalTemplate.Domain/"]
COPY ["GbsoDevExagonalTemplate.Dtos/GbsoDevExagonalTemplate.Dtos.csproj", "GbsoDevExagonalTemplate.Dtos/"]
COPY ["GbsoDevExagonalTemplate.Infrastructure/GbsoDevExagonalTemplate.Infrastructure.csproj", "GbsoDevExagonalTemplate.Infrastructure/"]
COPY ["GbsoDevExagonalTemplate.Application/GbsoDevExagonalTemplate.Application.csproj", "GbsoDevExagonalTemplate.Application/"]
COPY ["GbsoDevExagonalTemplate.Data.EfCore/GbsoDevExagonalTemplate.Data.EfCore.csproj", "GbsoDevExagonalTemplate.Data.EfCore/"]
RUN dotnet restore "GbsoDevExagonalTemplate.Api/GbsoDevExagonalTemplate.Api.csproj"
COPY . .
WORKDIR "/src/GbsoDevExagonalTemplate.Api"
RUN dotnet build "GbsoDevExagonalTemplate.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GbsoDevExagonalTemplate.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GbsoDevExagonalTemplate.Api.dll"]