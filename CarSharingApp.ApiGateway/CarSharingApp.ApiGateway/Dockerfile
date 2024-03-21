FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarSharingApp.ApiGateway/CarSharingApp.ApiGateway.csproj", "CarSharingApp.ApiGateway/"]
RUN dotnet restore "CarSharingApp.ApiGateway/CarSharingApp.ApiGateway.csproj"
COPY . .
WORKDIR "/src/CarSharingApp.ApiGateway"
RUN dotnet build "CarSharingApp.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharingApp.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharingApp.ApiGateway.dll"]
