FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarSharingApp.DealService/CarSharingApp.DealService.API/CarSharingApp.DealService.API.csproj", "src/CarSharingApp.DealService.API/"]
COPY ["CarSharingApp.DealService/CarSharingApp.DealService.BusinessLogic/CarSharingApp.DealService.BusinessLogic.csproj", "src/CarSharingApp.DealService.BusinessLogic/"]
COPY ["CarSharingApp.DealService/CarSharingApp.DealService.DataAccess/CarSharingApp.DealService.DataAccess.csproj", "src/CarSharingApp.DealService.DataAccess/"]
COPY ["CarSharingApp.DealService/CarSharingApp.DealService.Shared/CarSharingApp.DealService.Shared.csproj", "src/CarSharingApp.DealService.Shared/"]
COPY ["CarSharingApp.Common/CarSharingApp.Common/CarSharingApp.Common.csproj", "src/CarSharingApp.Common/"]

WORKDIR /src

RUN dotnet restore "src/CarSharingApp.DealService.API/CarSharingApp.DealService.API.csproj"
COPY ./CarSharingApp.DealService/ ./CarSharingApp.DealService
COPY ./CarSharingApp.Common/CarSharingApp.Common/ ./CarSharingApp.Common/CarSharingApp.Common

WORKDIR "/src/CarSharingApp.DealService/CarSharingApp.DealService.API"
RUN dotnet build "CarSharingApp.DealService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.DealService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.DealService.API.dll"]
