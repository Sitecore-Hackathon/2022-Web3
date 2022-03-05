FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web3-Operator.sln", "./"]
COPY ["src/api/Api.csproj", "./src/api/"]
COPY ["src/core/Core.csproj", "./src/core/"]
COPY ["src/docker-engine/DockerEngineOperator.csproj", "./src/docker-engine/"]
RUN dotnet restore
COPY . .
RUN dotnet build "src/api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/api/Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]