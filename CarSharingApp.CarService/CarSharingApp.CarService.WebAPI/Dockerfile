FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarSharingApp.CarService/CarSharingApp.CarService.WebAPI/CarSharingApp.CarService.WebAPI.csproj", "CarSharingApp.CarService/CarSharingApp.CarService.WebAPI/"]
COPY ["CarSharingApp.CarService/CarSharingApp.CarService.Application/CarSharingApp.CarService.Application.csproj", "CarSharingApp.CarService/CarSharingApp.CarService.Application/"]
COPY ["CarSharingApp.CarService/CarSharingApp.CarService.Infrastructure/CarSharingApp.CarService.Infrastructure.csproj", "CarSharingApp.CarService/CarSharingApp.CarService.Infrastructure/"]
COPY ["CarSharingApp.CarService/CarSharingApp.CarService.Domain/CarSharingApp.CarService.Domain.csproj", "CarSharingApp.CarService/CarSharingApp.CarService.Domain/"]
COPY ["CarSharingApp.Common/CarSharingApp.Common/CarSharingApp.Common.csproj", "CarSharingApp.Common/CarSharingApp.Common/"]

RUN dotnet restore "CarSharingApp.CarService/CarSharingApp.CarService.WebAPI/CarSharingApp.CarService.WebAPI.csproj"
COPY ./CarSharingApp.CarService/ ./CarSharingApp.CarService
COPY ./CarSharingApp.Common/CarSharingApp.Common/ ./CarSharingApp.Common/CarSharingApp.Common
WORKDIR "/src/CarSharingApp.CarService/CarSharingApp.CarService.WebAPI"
RUN dotnet build "CarSharingApp.CarService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.CarService.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.CarService.WebAPI.dll"]
