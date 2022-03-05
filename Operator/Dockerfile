FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web3-Operator.sln", "./"]
COPY ["src/Api/Api.csproj", "./src/Api/"]
COPY ["src/Core/Core.csproj", "./src/Core/"]
COPY ["src/DockerEngineOperator/DockerEngineOperator.csproj", "./src/DockerEngineOperator/"]
RUN dotnet restore
COPY . .
RUN dotnet build "src/DockerEngineOperator/DockerEngineOperator.csproj" -c Release -o /app/build
RUN dotnet build "src/Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/DockerEngineOperator/DockerEngineOperator.csproj" -c Release -o /app/publish
RUN dotnet publish "src/Api/Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]