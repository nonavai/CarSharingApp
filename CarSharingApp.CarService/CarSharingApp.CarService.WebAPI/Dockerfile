FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarSharingApp.CarService.WebAPI/CarSharingApp.CarService.WebAPI.csproj", "CarSharingApp.CarService.WebAPI/"]
RUN dotnet restore "CarSharingApp.CarService.WebAPI/CarSharingApp.CarService.WebAPI.csproj"
COPY . .
WORKDIR "/src/CarSharingApp.CarService.WebAPI"
RUN dotnet build "CarSharingApp.CarService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.CarService.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.CarService.WebAPI.dll"]
