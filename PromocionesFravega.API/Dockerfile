#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PromocionesFravega.API/PromocionesFravega.API.csproj", "PromocionesFravega.API/"]
RUN dotnet restore "PromocionesFravega.API/PromocionesFravega.API.csproj"
COPY . .
WORKDIR "/src/PromocionesFravega.API"
RUN dotnet build "PromocionesFravega.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PromocionesFravega.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PromocionesFravega.API.dll"]