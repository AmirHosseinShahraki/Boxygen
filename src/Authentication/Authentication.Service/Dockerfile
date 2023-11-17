FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Authentication/Authentication.Service/Authentication.Service.csproj", "src/Authentication/Authentication.Service/"]
RUN dotnet restore "src/Authentication/Authentication.Service/Authentication.Service.csproj"
COPY . .
WORKDIR "/src/src/Authentication/Authentication.Service"
RUN dotnet build "Authentication.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Service.dll"]
