# Sitecore ASP.NET Rendering SDK example solution

Basic ASP.NET Core project with the [Sitecore ASP.NET Rendering SDK](https://doc.sitecore.com/xp/en/developers/100/developer-tools/sitecore-asp-net-rendering-sdk.html) wired up and a example site implementation.

## Requirements

- Windows, WSL/Linux or macOS
- .NET 6.0 SDK

## Run and develop

Clone and install Sitecore CLI:

1. `dotnet tool restore`

Then to configure a operator endpoint and start new instance using our plugin:

1. `dotnet sitecore instance init --url http://operator-127-0-0-1.nip.io`
1. `dotnet sitecore instance start -n 'example' --pwd 'b'`, returns the url of your new instance.

Login to instance (notice the `--cm` and `--auth` urls are the same):

1. `dotnet sitecore login --insecure --cm http://example-127-0-0-1.nip.io --auth http://example-127-0-0-1.nip.io --client-credentials true --allow-write true --client-id "sitecore\admin" --client-secret "b"`

Now you can push example items and start hot reload (default browser should open [http://localhost:5000](http://localhost:5000)):

1. `dotnet sitecore ser push --publish`
1. `$env:SITECORE__INSTANCEURI="http://example-127-0-0-1.nip.io"` (powershell) or `export SITECORE__INSTANCEURI=http://example-127-0-0-1.nip.io` (bash) to configure the rendering host to use the new instance url.
1. `dotnet watch --project ./src/ExampleApp.RenderingHost/`

## Tips and tricks

- Stop and remove instance: `dotnet sitecore instance stop -n 'example'`
- List all instances: `dotnet sitecore instance list`
- Tail Sitecore log from instance: `docker logs --follow --tail 100 sc_example`
