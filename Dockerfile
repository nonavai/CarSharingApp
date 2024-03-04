FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["./CarSharingApp.DealService/CarSharingApp.DealService.DataAccess/CarSharingApp.DealService.DataAccess.csproj", "CarSharingApp.DealService/CarSharingApp.DealService.DataAccess/"]
COPY ["./CarSharingApp.DealService/CarSharingApp.DealService.BusinessLogic/CarSharingApp.DealService.BusinessLogic.csproj", "CarSharingApp.DealService/CarSharingApp.DealService.BusinessLogic/"]
COPY ["./CarSharingApp.DealService/CarSharingApp.DealService.API/CarSharingApp.DealService.API.csproj", "CarSharingApp.DealService/CarSharingApp.DealService.API/"]
COPY ["./CarSharingApp.Common/CarSharingApp.Common/CarSharingApp.Common.csproj", "CarSharingApp.Common/CarSharingApp.Common/"]

RUN dotnet restore "CarSharingApp.DealService/CarSharingApp.DealService.API/CarSharingApp.DealService.API.csproj"

COPY . .

WORKDIR "/src"
RUN dotnet build "CarSharingApp.DealService/CarSharingApp.DealService.API/CarSharingApp.DealService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.DealService/CarSharingApp.DealService.API/CarSharingApp.DealService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.DealService.API.dll"]
