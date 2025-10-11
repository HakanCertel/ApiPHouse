# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/YayinEviApi.API/YayinEviApi.API.csproj", "Presentation/YayinEviApi.API/"] 
COPY ["Core/YayinEviApi.Application/YayinEviApi.Application.csproj", "Core/YayinEviApi.Application/"] 
COPY ["Core/YayinEviApi.Domain/YayinEviApi.Domain.csproj", "Core/YayinEviApi.Domain/"] 
COPY ["Infrastructure/YayinEviApi.Infrastructure/YayinEviApi.Infrastructure.csproj", "Infrastructure/YayinEviApi.Infrastructure/"]
COPY ["./Infrastructure/YayinEviApi.Persistence/YayinEviApi.Persistence.csproj", "Infrastructure/YayinEviApi.Persistence/"]
COPY ["./Infrastructure/YayinEviApi.SignalR/YayinEviApi.SignalR.csproj", "Infrastructure/YayinEviApi.SignalR/"]
RUN dotnet restore "./Presentation/YayinEviApi.API/YayinEviApi.API.csproj"
COPY . .
WORKDIR "/src/Presentation/YayinEviApi.API"
RUN dotnet build "./YayinEviApi.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YayinEviApi.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YayinEviApi.API.dll"]