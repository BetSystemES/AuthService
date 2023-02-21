FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AuthService.Grpc/AuthService.Grpc.csproj", "src/AuthService.Grpc/"]
COPY ["src/AuthService.BusinessLogic/AuthService.BusinessLogic.csproj", "src/AuthService.BusinessLogic/"]
COPY ["src/AuthService.DataAccess/AuthService.DataAccess.csproj", "src/AuthService.DataAccess/"]
RUN dotnet restore "src/AuthService.Grpc/AuthService.Grpc.csproj"
COPY . .
WORKDIR "/src/src/AuthService.Grpc"
RUN dotnet build "AuthService.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthService.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.Grpc.dll"]