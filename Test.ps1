# setup
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

# test glitterfish cm
$glitterfishPath = (Join-Path $PSScriptRoot "\glitterfish")

Write-Host "### Testing using working directory '$glitterfishPath'..."

Push-Location -Path $glitterfishPath

$glitterfishContainerName = "sitecore-glitterfish-cm"
$glitterfishImage = "sitecore-glitterfish-cm:latest"
$glitterfishPort = 7777

try
{
    if (-not(docker image ls $glitterfishImage --quiet))
    {
        throw "Image '$glitterfishImage' not found, please run .\Build.ps1 first."
    }

    Write-Host "### Starting container..."

    docker run --rm -d --name $glitterfishContainerName -p $glitterfishPort`:80 -e SITECORE_ADMIN_PASSWORD=b $glitterfishImage

    Write-Host "### Testing health..."

    $healthStatus = (Invoke-WebRequest http://localhost:$glitterfishPort/healthz/ready -UseBasicParsing).StatusCode

    if ($healthStatus -ne 200)
    {
        throw "Sitecore health check failed, status was '$healthStatus'."
    }

    Write-Host "### Testing Sitecore CLI communications..."

    dotnet tool restore
    dotnet sitecore login --insecure --cm http://localhost:$glitterfishPort --auth http://localhost:$glitterfishPort --client-credentials true --allow-write true --client-id "sitecore\admin" --client-secret "b" --trace
    dotnet sitecore ser pull --verbose --trace
}
finally
{
    Write-Host "### Shutting down..."

    docker rm $glitterfishContainerName --force

    Pop-Location
}

# done
Write-Host "### Done."