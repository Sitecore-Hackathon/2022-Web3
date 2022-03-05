# Sitecore ASP.NET Rendering SDK example solution

...

## Run

Clone and open on host or in WSL (adjust `--auth` and `--cm` urls accordingly) and then:

1. `dotnet tool restore`
1. `dotnet sitecore login --insecure --cm http://localhost:44090 --auth http://localhost:44090 --client-credentials true --allow-write true --client-id "sitecore\admin" --client-secret "b" --trace`
1. `dotnet sitecore ser push --publish`

Run on Docker (Linux):

1. `docker-compose up --build` (hot reload works here as well)

Or if you wish to run directly on host or in WSL:

1. `dotnet watch --project ./src/ExampleApp.RenderingHost/`
