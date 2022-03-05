FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app
COPY . /app

RUN dotnet restore

ENTRYPOINT [ "dotnet", "watch", "run", "--no-restore", "--urls", "http://+:5000", "--project", "./src/ExampleApp.RenderingHost/" ]