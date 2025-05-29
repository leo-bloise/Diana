function CreateLocalSourceIfNeeded() {
    $exists = dotnet nuget list source | Select-String "LocalSource"
    if($exists) {
        Write-Host "✅ Local NuGet Source already exists."
    } else {
        Write-Host "📁 Creating local NuGet source..."
        dotnet nuget add source "$env:USERPROFILE\LocalNugetSource" -n LocalFeed
        Write-Host "✅ Local NuGet Source created successfully."
    }
}
CreateLocalSourceIfNeeded
Write-Host "📦 Packing Diana.Core as a local NuGet package..."
dotnet pack -c Release -o ./package ./Diana.Core/Diana.Core.csproj -v quiet
Write-Host "👤 User Profile: $env:USERPROFILE"
$sourcePath = ".\package\Diana.Core*.nupkg"
$destinationPath = "$env:USERPROFILE\LocalNugetSource"
if (-not (Test-Path -Path $destinationPath)) {
    New-Item -ItemType Directory -Path $destinationPath | Out-Null
    Write-Host "📁 Created local NuGet feed folder: $destinationPath"
}
Get-ChildItem -Path $sourcePath -Recurse | Move-Item -Destination $destinationPath -Force
Write-Host "✅ Package moved to local NuGet feed: $destinationPath"
