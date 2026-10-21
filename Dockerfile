# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app
# Copy csproj and restore as distinct layers
COPY host/Byd.GtfsManagement.HttpApi.Host/Byd.GtfsManagement.HttpApi.Host.csproj ./
COPY NuGet.Config ./
RUN dotnet restore && rm NuGet.Config
# Copy everything else and build
COPY . ./
RUN dotnet publish host/Byd.GtfsManagement.HttpApi.Host/Byd.GtfsManagement.HttpApi.Host.csproj -c Release -o out
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
ARG GTFS_VALIDATOR_VERSION=8.0.1
# GTFS dogrulamasi MobilityData CLI'i ile yapilir: Java JAVA_HOME'dan, jar host'un yanindaki gtfs-validator klasorunden okunur.
COPY --from=eclipse-temurin:17-jre /opt/java/openjdk /opt/java/openjdk
ENV JAVA_HOME=/opt/java/openjdk
WORKDIR /app
COPY --from=build-env /app/out .
ADD --chmod=644 https://github.com/MobilityData/gtfs-validator/releases/download/v${GTFS_VALIDATOR_VERSION}/gtfs-validator-${GTFS_VALIDATOR_VERSION}-cli.jar gtfs-validator/gtfs-validator-cli.jar
ENTRYPOINT ["dotnet", "Byd.GtfsManagement.HttpApi.Host.dll"]
