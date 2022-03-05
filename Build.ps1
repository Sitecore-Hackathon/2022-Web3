# setup
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

# build glitterfish cm image
$glitterfishPath = (Join-Path $PSScriptRoot "\glitterfish")

Write-Host "### Building using working directory '$glitterfishPath'..."

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