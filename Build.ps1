# setup
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

# build glitterfish cm image
$glitterfishPath = (Join-Path $PSScriptRoot "\glitterfish")

$licensePath = Join-Path $glitterfishPath "docker\build\cm\license.xml"
If (-not (Test-Path $licensePath)) {
    Write-Error "Missing Sitecore license at $licensePath"
}

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
Write-Host "### build operator"
$operatorPath = Join-Path $PSScriptRoot "operator"
Push-Location -Path $operatorPath

try {
    docker-compose build

    $LASTEXITCODE -ne 0 | Where-Object { $_ } | ForEach-Object { throw "Build failed." }
} finally {
    Pop-Location
}

# build cli plugin
Write-Host "### TODO: build cli plugin"

# done
Write-Host "### Done."