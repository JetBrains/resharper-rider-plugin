Param(
    $RootSuffix = "SamplePlugin",
    $Version = "1.0.0"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

. ".\settings.ps1"

$UserProjectXmlFile = "$SourceBasePath\$PluginId\$PluginId.csproj.user"

if (!(Test-Path "$UserProjectXmlFile")) {
    # Determine download link
    $ReleaseUrl = "https://data.services.jetbrains.com/products/releases?code=RSU&type=eap&type=release"
    $DownloadLink = [uri] $(Invoke-WebRequest -UseBasicParsing $ReleaseUrl | ConvertFrom-Json).RSU[0].downloads.windows.link
    
    # Calculate version
    $VersionSplit = $DownloadLink.Segments[-1].Split(".")
    $VersionMajor = $VersionSplit[2]
    $VersionMinor = $VersionSplit[3]
    if ($VersionSplit.Count -eq 5)              { $SdkVersion = "$($VersionMajor).$($VersionMinor).0" }
    elseif ($VersionSplit[4].StartsWith("EAP")) { $SdkVersion = "$($VersionMajor).$($VersionMinor).0-*" }
    else                                        { $SdkVersion = "$($VersionMajor).$($VersionMinor).$($VersionSplit[4])" }
    $SdkVersionShort = "$($VersionMajor[2])$($VersionMajor[3])$($VersionMinor)"

    # Download installer
    $InstallerFile = "$PSScriptRoot\build\installer\$($DownloadLink.Segments[-1])"
    if (!(Test-Path $InstallerFile)) {
        mkdir -Force $(Split-Path $InstallerFile -Parent) > $null
        Write-Output "Downloading $SdkVersion installer from $DownloadLink"
        (New-Object System.Net.WebClient).DownloadFile($DownloadLink, $InstallerFile)
    } else {
        Write-Output "Using cached installer from $InstallerFile"
    }

    # Execute installer
    Write-Output "Installing experimental hive"
    Invoke-Exe $InstallerFile "/VsVersion=$VisualStudioMajorVersion.0" "/SpecificProductNames=ReSharper" "/Hive=$RootSuffix" "/Silent=True"

    $PluginRepository = "$env:LOCALAPPDATA\JetBrains\plugins"
    $Installations = @(Get-ChildItem "$env:APPDATA\JetBrains\ReSharperPlatformVs$VisualStudioMajorVersion\v$($SdkVersionShort)_$VisualStudioInstanceId$RootSuffix\NuGet.Config")
    if ($Installations.Count -ne 1) { Write-Error "Found no or multiple installation directories: $Installations" }
    $InstallationDirectory = $Installations.Directory
    Write-Host "Found installation directory at $InstallationDirectory"

    # Adapt packages.config
    if (Test-Path "$InstallationDirectory\packages.config") {
        $PackagesXml = [xml] (Get-Content "$InstallationDirectory\packages.config")
    } else {
        $PackagesXml = [xml] ("<?xml version=`"1.0`" encoding=`"utf-8`"?><packages></packages>")
    }

    if ($null -eq $PackagesXml.SelectSingleNode(".//package[@id='$PluginId']/@id")) {
        $PluginNode = $PackagesXml.CreateElement('package')
        $PluginNode.setAttribute("id", "$PluginId")
        $PluginNode.setAttribute("version", "$Version")

        $PackagesNode = $PackagesXml.SelectSingleNode("//packages")
        $PackagesNode.AppendChild($PluginNode) > $null

        $PackagesXml.Save("$InstallationDirectory\packages.config")
    }

    # Install plugin
    Invoke-Exe $MSBuildPath "/t:Restore;Rebuild;Pack" "$SolutionPath" "/v:minimal" "/p:PackageVersion=$Version" "/p:PackageOutputPath=`"$OutputDirectory`""
    Invoke-Exe $NuGetPath install $PluginId -OutputDirectory "$PluginRepository" -Source "$OutputDirectory" -DependencyVersion Ignore

    Write-Output "Re-installing experimental hive"
    Invoke-Exe "$InstallerFile" "/VsVersion=$VisualStudioMajorVersion.0" "/SpecificProductNames=ReSharper" "/Hive=$RootSuffix" "/Silent=True"

    # Adapt user project file
    $HostIdentifier = "$($InstallationDirectory.Parent.Name)_$($InstallationDirectory.Name.Split('_')[-1])"
    
    Set-Content -Path "$UserProjectXmlFile" -Value "<Project><PropertyGroup><HostFullIdentifier></HostFullIdentifier></PropertyGroup></Project>"

    $ProjectXml = [xml] (Get-Content "$UserProjectXmlFile")
    $HostIdentifierNode = $ProjectXml.SelectSingleNode(".//HostFullIdentifier")
    $HostIdentifierNode.InnerText = $HostIdentifier
    $ProjectXml.Save("$UserProjectXmlFile")

    # Update Plugin.props
    $PluginPropsFile = "$SourceBasePath\Plugin.props"
    $PluginPropsXml = [xml] (Get-Content "$PluginPropsFile")
    $SdkVersionNode = $PluginPropsXml.SelectSingleNode(".//SdkVersion")
    $SdkVersionNode.InnerText = $SdkVersion
    $PluginPropsXml.Save("$PluginPropsFile")
} else {
    Write-Warning "Plugin is already installed. To trigger reinstall, delete $UserProjectXmlFile."
}

Invoke-Exe $MSBuildPath "/t:Restore;Rebuild" "$SolutionPath" "/v:minimal"
Invoke-Exe $DevEnvPath "/rootSuffix $RootSuffix" "/ReSharper.Internal"