:; SCRIPT_DIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd)
:; ${SCRIPT_DIR}/install.cmd
:; dotnet new resharper-rider-plugin --force --name foobar "$@"
:; exit $?

call %~dp0install.cmd
dotnet new resharper-rider-plugin --force --name foobar %*
