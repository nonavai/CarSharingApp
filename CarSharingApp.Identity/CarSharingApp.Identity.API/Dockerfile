FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarSharingApp.Identity/CarSharingApp.Identity.API/CarSharingApp.Identity.API.csproj", "CarSharingApp.Identity/CarSharingApp.Identity.API/"]
COPY ["CarSharingApp.Identity/CarSharingApp.Identity.BusinessLogic/CarSharingApp.Identity.BusinessLogic.csproj", "CarSharingApp.Identity/CarSharingApp.Identity.BusinessLogic/"]
COPY ["CarSharingApp.Identity/CarSharingApp.Identity.DataAccess/CarSharingApp.Identity.DataAccess.csproj", "CarSharingApp.Identity/CarSharingApp.Identity.DataAccess/"]
COPY ["CarSharingApp.Identity/CarSharingApp.Identity.Shared/CarSharingApp.Identity.Shared.csproj", "CarSharingApp.Identity/CarSharingApp.Identity.Shared/"]
COPY ["CarSharingApp.Common/CarSharingApp.Common/CarSharingApp.Common.csproj", "CarSharingApp.Common/CarSharingApp.Common/"]

RUN dotnet restore "CarSharingApp.Identity/CarSharingApp.Identity.API/CarSharingApp.Identity.API.csproj"
COPY ./CarSharingApp.Identity/ ./CarSharingApp.Identity
COPY ./CarSharingApp.Common/CarSharingApp.Common/ ./CarSharingApp.Common/CarSharingApp.Common
WORKDIR "/src/CarSharingApp.Identity/CarSharingApp.Identity.API"
RUN dotnet build "CarSharingApp.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.Identity.API.dll"]
