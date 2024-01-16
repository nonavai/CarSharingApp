FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarSharingApp.NotificationService.API/CarSharingApp.NotificationService.API.csproj", "CarSharingApp.NotificationService.API/"]
RUN dotnet restore "CarSharingApp.NotificationService.API/CarSharingApp.NotificationService.API.csproj"
COPY . .
WORKDIR "/src/CarSharingApp.NotificationService.API"
RUN dotnet build "CarSharingApp.NotificationService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.NotificationService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.NotificationService.API.dll"]
