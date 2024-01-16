FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarSharingApp.Identity.API/CarSharingApp.Identity.API.csproj", "CarSharingApp.Identity.API/"]
RUN dotnet restore "CarSharingApp.Identity.API/CarSharingApp.Identity.API.csproj"
COPY . .
WORKDIR "/src/CarSharingApp.Identity.API"
RUN dotnet build "CarSharingApp.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.Identity.API.dll"]
