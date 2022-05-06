:; set -eo pipefail
:; SCRIPT_DIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd)
:; dotnet new --uninstall ${SCRIPT_DIR}/content
:; dotnet new --install ${SCRIPT_DIR}/content
:; exit $?

dotnet new --uninstall %~dp0content
dotnet new --install %~dp0content
