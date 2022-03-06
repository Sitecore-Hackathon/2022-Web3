# setup
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

# prerequisites
Write-Host "This project requires to map port 80 to a local traefik instance"

$iisStatus = Get-Service -Name w3svc -ErrorAction SilentlyContinue | Select-Object -ExpandProperty Status

if ("${iisStatus}" -eq "Running")
{
    Write-Host "We noticed that you have IIS running - as a precaution, we will stop IIS service (this requires elevated mode)`n" -ForegroundColor Yellow

    Stop-Service -Name w3svc -Verbose
}

# start
$operatorPath = Join-Path $PSScriptRoot "\operator"

try
{
    Push-Location -Path $operatorPath

    $env:NETWORK_NAME = "operatornet"
    $id = $(docker network ls -f name=${env:NETWORK_NAME} -q)

    if ("${id}" -eq "")
    {
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
