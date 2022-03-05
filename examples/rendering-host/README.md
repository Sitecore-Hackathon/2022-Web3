# Sitecore ASP.NET Rendering SDK example solution

Basic ASP.NET Core project with the [Sitecore ASP.NET Rendering SDK](https://doc.sitecore.com/xp/en/developers/100/developer-tools/sitecore-asp-net-rendering-sdk.html) wired up and a example site implementation.

## Run

Clone and open on host, then:

1. `dotnet tool restore`
1. TODO: use operator plugin to acquire instance...
1. TODO: update ZZZ with instance url
1. `dotnet sitecore login --insecure --cm ZZZ --auth ZZZ --client-credentials true --allow-write true --client-id "sitecore\admin" --client-secret "b"`
1. `dotnet sitecore ser push --publish`

Run in Docker (Linux) on host or WSL:

1. `docker-compose up --build` (hot reload works here as well)

Or if you wish to run *directly* on host or WSL:

1. `dotnet watch --project ./src/ExampleApp.RenderingHost/`

Then browse [http://localhost:5000](http://localhost:5000).
