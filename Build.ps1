# setup
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

if ($null -eq (Get-Command "docker-compose" -ErrorAction "SilentlyContinue"))
{
    throw "docker-compose.exe was not found."
}

# build glitterfish cm image
$glitterfishPath = (Join-Path $PSScriptRoot "\glitterfish")

Write-Host "### Building '$glitterfishPath'..."

Push-Location -Path $glitterfishPath

try
{
    docker-compose build

    $LASTEXITCODE -ne 0 | Where-Object { $_ } | ForEach-Object { throw "Build failed." }
}
finally
{
    Pop-Location
}

# build operator
Write-Host "### TODO: build operator"

# build cli plugin
Write-Host "### TODO: build cli plugin"

# done
Write-Host "### Done."