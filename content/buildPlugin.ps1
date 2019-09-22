Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

. ".\settings.ps1"

Invoke-Exe dotnet msbuild "/t:Restore;Rebuild;Pack" "$SolutionPath"