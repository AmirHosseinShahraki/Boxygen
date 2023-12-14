FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Preferences/Preferences.API/Preferences.API.csproj", "src/Preferences/Preferences.API/"]
RUN dotnet restore "src/Preferences/Preferences.API/Preferences.API.csproj"
COPY . .
WORKDIR "/src/src/Preferences/Preferences.API"
RUN dotnet build "Preferences.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Preferences.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Preferences.API.dll"]
