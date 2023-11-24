FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Profile/Profile.API/Profile.API.csproj", "src/Profile/Profile.API/"]
RUN dotnet restore "src/Profile/Profile.API/Profile.API.csproj"
COPY . .
WORKDIR "/src/src/Profile/Profile.API"
RUN dotnet build "Profile.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profile.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile.API.dll"]
