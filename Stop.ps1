# setup
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

# start, stopping...
$operatorPath = Join-Path $PSScriptRoot "\operator"

try
{
    Push-Location -Path $operatorPath

    docker-compose down

    (docker ps --filter name=sc_* -q) | ForEach-Object {
        docker rm --force $_
    }
}
finally
{
    Pop-Location
}
