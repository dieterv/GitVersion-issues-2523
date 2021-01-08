###########################################################################
# CONFIGURE .NET CORE CLI
###########################################################################
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
$env:DOTNET_CLI_TELEMETRY_OPTOUT=1
$env:DOTNET_NOLOGO=$true

###########################################################################
# RUN BUILD SCRIPT
###########################################################################
# Write-Host "Restoring global tools..."
# Invoke-Expression "dotnet tool restore"
# if($LASTEXITCODE -ne 0) {
#     Pop-Location;
#     exit $LASTEXITCODE;
# }

Push-Location
Set-Location build\source

Write-Host "Preparing Cake.Frosting build runner..."
Invoke-Expression "dotnet restore"
if($LASTEXITCODE -ne 0) {
    Pop-Location;
    exit $LASTEXITCODE;
}

Write-Host "Running Cake.Frosting build runner..."
Write-Host "dotnet run -- $args"
Invoke-Expression "dotnet run -- $args"
if($LASTEXITCODE -ne 0) {
    Pop-Location;
    exit $LASTEXITCODE;
}

Pop-Location
