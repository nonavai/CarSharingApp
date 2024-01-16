FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarSharingApp.Identity.Shared/CarSharingApp.Identity.Shared.csproj", "CarSharingApp.Identity.Shared/"]
RUN dotnet restore "CarSharingApp.Identity.Shared/CarSharingApp.Identity.Shared.csproj"
COPY . .
WORKDIR "/src/CarSharingApp.Identity.Shared"
RUN dotnet build "CarSharingApp.Identity.Shared.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.Identity.Shared.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.Identity.Shared.dll"]
