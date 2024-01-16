FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarSharingApp.DealService.API/CarSharingApp.DealService.API.csproj", "CarSharingApp.DealService.API/"]
RUN dotnet restore "CarSharingApp.DealService.API/CarSharingApp.DealService.API.csproj"
COPY . .
WORKDIR "/src/CarSharingApp.DealService.API"
RUN dotnet build "CarSharingApp.DealService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.DealService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.DealService.API.dll"]
