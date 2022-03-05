# setup
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

# start
$operatorPath = Join-Path $PSScriptRoot "\operator"

Push-Location -Path $operatorPath

try
{
    $env:NETWORK_NAME = "operatornet"
    $id = $(docker network ls -f name=${env:NETWORK_NAME} -q )
    if("${id}" -eq "") {
        docker network create -d nat --attachable ${env:NETWORK_NAME}
    }
    docker-compose down
    docker-compose up -d

    $LASTEXITCODE -ne 0 | Where-Object { $_ } | ForEach-Object { throw "Start failed." }
}
finally
{
    Pop-Location
}

