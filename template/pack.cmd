:; set -eo pipefail
:; SCRIPT_DIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd)
:; git clean -fdx ${SCRIPT_DIR}
:; dotnet pack ${SCRIPT_DIR}/JetBrains.ReSharper.SamplePlugin.nuspec -NoPackageAnalysis -NoDefaultExcludes "$@"
:; exit $?

git clean -fdx %~dp0
%~dp0content\tools\nuget.exe pack %~dp0JetBrains.ReSharper.SamplePlugin.nuspec -NoPackageAnalysis -NoDefaultExcludes %*
