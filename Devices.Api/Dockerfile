# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Devices.sln ./
COPY Devices.Api/Devices.Api.csproj Devices.Api/
COPY Devices.Domain/Devices.Domain.csproj Devices.Domain/
COPY Devices.Infrastructure/Devices.Infrastructure.csproj Devices.Infrastructure/
RUN dotnet restore Devices.sln

COPY . .
RUN dotnet publish Devices.Api/Devices.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Devices.Api.dll"]
