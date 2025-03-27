#!/bin/bash

log_file="$PWD/jb-update-build-$(date +%Y-%m-%d).log"

for dir in pl*; do
    if [ ! -d "$dir" ]; then
        continue
    fi

    cd "$dir" || exit

    echo "Updating build: $PWD"
    jb_plugin_update_build 1>/dev/null 2>>"$log_file"

    cd ..
done
