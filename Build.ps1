# setup
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

# build glitterfish cm image
$glitterfishPath = Join-Path $PSScriptRoot "\glitterfish"
$licensePath = Join-Path $glitterfishPath "docker\build\cm\license.xml"

if (-not(Test-Path $licensePath))
{
    throw "Missing Sitecore license at '$licensePath'."
}

Write-Host "### Building image using working directory '$glitterfishPath'..."

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

# build operator api
Write-Host "### Building operator API"

$operatorPath = Join-Path $PSScriptRoot "operator"

Push-Location -Path $operatorPath

try
{
    docker-compose build

    $LASTEXITCODE -ne 0 | Where-Object { $_ } | ForEach-Object { throw "Build failed." }
}
finally
{
    Pop-Location
}

# build operator cli plugin
Write-Host "### Building Sitecore CLI operator plugin"

$operatorPath = Join-Path $PSScriptRoot "operator/src/sitecore-cli"

Push-Location -Path $operatorPath

try
{
    dotnet restore

    $LASTEXITCODE -ne 0 | Where-Object { $_ } | ForEach-Object { throw "Build failed." }

    dotnet build

    $LASTEXITCODE -ne 0 | Where-Object { $_ } | ForEach-Object { throw "Build failed." }
}
finally
{
    Pop-Location
}

# done
Write-Host "### Done."