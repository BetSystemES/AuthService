FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AuthService.DatabaseMigrator/AuthService.DatabaseMigrator.csproj", "src/AuthService.DatabaseMigrator/"]
COPY ["src/AuthService.DataAccess/AuthService.DataAccess.csproj", "src/AuthService.DataAccess/"]
COPY ["src/AuthService.BusinessLogic/AuthService.BusinessLogic.csproj", "src/AuthService.BusinessLogic/"]
RUN dotnet restore "src/AuthService.DatabaseMigrator/AuthService.DatabaseMigrator.csproj"
COPY . .
WORKDIR "/src/src/AuthService.DatabaseMigrator"
RUN dotnet build "AuthService.DatabaseMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthService.DatabaseMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
