# Pre-requisites
1. Nuget.exe

# Run locally 
`dotnet run --project DotNet8.csproj`

# Run in an offline

## AzDo YAML
### nuget restore from AzDo feed using NuGetCommand@2

## PowerShell on agent
`$projectPackages = (Join-Path Get-Location packages)
$localPackages = (Join-Path $env:USERPROFILE .nuget\packages)`

### list local sources
must have project nuget.config, config value don't matter, they will be overidden
`nuget sources list`

### change local source to local
`nuget sources update -Name nuget -Source $localPackages`

### copy packages to project directory
`nuget restore DotNet8.csproj -PackagesDirectory packages`

### now copy to second server

## PowerShell on second server
### change local source to packages
`nuget sources update -Name nuget -Source $projectPackages`

### restore from packages folder
`dotnet clean
nuget restore DotNet8.csproj -PackagesDirectory packages -Source $projectPackages -Verbosity detailed`

### run the app with environmental config
`dotnet run --project DotNet8.csproj --no-restore --Environment=AT` 
