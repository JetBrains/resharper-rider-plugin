[![JetBrains Official](https://img.shields.io/badge/project-official-brightgreen.svg?style=flat-square&label=&colorA=3c3c3c&colorB=ff8a2c&logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48c3ZnIHZlcnNpb249IjEuMSIgaWQ9IkxheWVyXzEiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIHg9IjBweCIgeT0iMHB4IiB3aWR0aD0iMTRweCIgaGVpZ2h0PSIxNHB4IiB2aWV3Qm94PSIwIDAgMTQgMTQiIGVuYWJsZS1iYWNrZ3JvdW5kPSJuZXcgMCAwIDE0IDE0IiB4bWw6c3BhY2U9InByZXNlcnZlIj48cmVjdCB4PSIxIiB5PSIxMiIgZmlsbD0iI0ZGRkZGRiIgd2lkdGg9IjciIGhlaWdodD0iMSIvPjxwYXRoIGZpbGw9IiNGRkZGRkYiIGQ9Ik0wLjMsNy4zbDEtMS4xYzAuNCwwLjUsMC44LDAuNywxLjMsMC43YzAuNiwwLDEtMC40LDEtMS4yVjFoMS42djQuN2MwLDAuOS0wLjIsMS41LTAuNywxLjlDNC4xLDguMSwzLjQsOC40LDIuNiw4LjRDMS41LDguNCwwLjgsNy45LDAuMyw3LjN6Ii8%2BPHBhdGggZmlsbD0iI0ZGRkZGRiIgZD0iTTYuOCwxaDMuNGMwLjgsMCwxLjUsMC4yLDEuOSwwLjZjMC4zLDAuMywwLjUsMC43LDAuNSwxLjJsMCwwYzAsMC44LTAuNCwxLjMtMSwxLjZDMTIuNSw0LjgsMTMsNS4zLDEzLDYuMmwwLDBjMCwxLjMtMS4xLDItMi43LDJINi44VjF6IE0xMSwzLjFjMC0wLjUtMC40LTAuNy0xLTAuN0g4LjR2MS41aDEuNUMxMC42LDMuOSwxMSwzLjcsMTEsMy4xTDExLDMuMXogTTEwLjIsNS4zSDguNHYxLjZoMS45YzAuNywwLDEuMS0wLjIsMS4xLTAuOGwwLDBDMTEuNCw1LjYsMTEuMSw1LjMsMTAuMiw1LjN6Ii8%2BPHJlY3QgeD0iMSIgeT0iMTIiIGZpbGw9IiNGRkZGRkYiIHdpZHRoPSI3IiBoZWlnaHQ9IjEiLz48L3N2Zz4%3D)](https://jetbrains.com/)

# Plugin Template for ReSharper and Rider

This repository defines a template for easy development of ReSharper and Rider plugins according to the official documentation for the [ReSharper SDK](https://www.jetbrains.com/help/resharper/sdk/README.html) and [IntelliJ SDK](http://www.jetbrains.org/intellij/sdk/docs/welcome.html).

## Prerequisites

When developing for Rider, [Java 11 Amazon Corretto](https://docs.aws.amazon.com/corretto/latest/corretto-11-ug/downloads-list.html) should be installed.

## Getting Started

Download the `JetBrains.ReSharper.SamplePlugin.*.nupkg` template package from the [releases page](https://github.com/matkoch/resharper-sampleplugin/releases) and invoke from the download directory:

```
dotnet new --install JetBrains.ReSharper.SamplePlugin --nuget-source ./
```

Afterwards, a new project can be created from the installed template. The `name` identifier should be letters-only:

```
dotnet new resharper-rider-plugin --name MyAwesomePlugin
```

This will create a new folder with all the structure ready to go and all identifiers, like namespaces, ids and file names, replaced with `MyAwesomePlugin`. Metadata including project website, description, author and others should be entered in `Plugin.props` and `plugins.xml`.

:warning: _The only place that currently needs to be updated manually is the `RIDER_PLUGIN_ID` in `README.md`, which you'll only get after uploading your Rider plugin the first time._

## Development

For general development, there are a couple of scripts/invocations worth knowing. Most importantly, to run and debug your plugin, invoke:

```
# For Rider
gradlew :runIde

# For ReSharper (VisualStudio)
powershell .\runVisualStudio.ps1
```

When starting Gradle tasks from inside IntelliJ IDEA, make sure that the Gradle settings are as follows:

<img src="./images/gradle-configuration01.png" width="600" />
<img src="./images/gradle-configuration02.png" width="600" />


If your Rider plugin requires a [model](https://www.jetbrains.com/help/resharper/sdk/Products/Rider.html) to share information between ReSharper backend and IntelliJ frontend, there is a sample protocol defined in `protocol` directory. To generate the Kotlin and C# implementation, call:

```
gradlew :rdgen
```

Opening the solution in Rider will automatically get you the corresponding [run configurations](https://www.jetbrains.com/help/rider/Creating_and_Editing_Run_Debug_Configurations.html):

<img src="./images/run-configurations.png" width="400" />

### Version Relevant Code

There are a couple of version identifiers that should always be updated synchronously:

- The `sdkVersion` variable in [build.gradle](https://github.com/matkoch/resharper-sampleplugin/blob/0b8fe5034141b7f731038acd8de3aa793f8bc630/content/build.gradle#L21) is responsible for download a certain Rider frontend distribution
- The `SdkVersion` property in [Plugin.props](https://github.com/matkoch/resharper-sampleplugin/blob/0b8fe5034141b7f731038acd8de3aa793f8bc630/content/src/dotnet/Plugin.props#L3) will affect the referenced `JetBrains.ReSharper.SDK` NuGet package and will also determine the `wave` version that is required for the Extension Manager in ReSharper
- The `runVisualStudio.ps1` script will always download the latest available installer for ReSharper - this can be either a normal release or early-access-program (EAP) release

Available versions are listed here for [ReSharper](https://www.nuget.org/packages/JetBrains.ReSharper.SDK) and [Rider](https://www.jetbrains.com/intellij-repository/snapshots) (under `com.jetbrains.intellij.rider`).

## Deployment

Both plugins can be published by calling:

```
# For Rider
gradlew :publishPlugin -PpluginVersion=<version> -Pusername=<username> -Ppassword=<password>

# For ReSharper (VisualStudio)
powershell ./publishPlugin.ps1 -Version <version> -ApiKey <ApiKey>
```
