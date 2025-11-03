FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY devkey.pfx devkey.pfx

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["outpost-logistics/outpost-logistics.csproj", "outpost-logistics/"]
RUN dotnet restore "outpost-logistics/outpost-logistics.csproj"
COPY . .
WORKDIR "/src/outpost-logistics"
RUN dotnet build "outpost-logistics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "outpost-logistics.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "outpost-logistics.dll"]
