# Sitecore ASP.NET Rendering SDK example solution

Basic ASP.NET Core project with the [Sitecore ASP.NET Rendering SDK](https://doc.sitecore.com/xp/en/developers/100/developer-tools/sitecore-asp-net-rendering-sdk.html) wired up and a example site implementation.

## Requirements

- Windows, WSL/Linux or macOS
- .NET 6.0 SDK

## Run and develop

Clone and install Sitecore CLI:

1. `dotnet tool restore`

Then to configure a operator and start new instance:

1. `dotnet sitecore instance init --url 'http://<OPERATOR_URL>'` TODO: add local url
1. `dotnet sitecore instance start -n 'winner' --pwd 'b'`
1. TODO: update ZZZ with instance url

Login to instance (notice the `--cm` and `--auth` urls are the same):

1. `dotnet sitecore login --insecure --cm ZZZ --auth ZZZ --client-credentials true --allow-write true --client-id "sitecore\admin" --client-secret "b"`

Now you can push example items and start hot reload (default browser should open [http://localhost:5000](http://localhost:5000)):

1. `dotnet sitecore ser push --publish`
1. `dotnet watch --project ./src/ExampleApp.RenderingHost/`
