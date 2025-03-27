#!/bin/bash

log_file="$PWD/jb-update-version-$(date +%Y-%m-%d).log"

for dir in *; do
    if [ ! -d "$dir" ]; then
        continue
    fi

    cd "$dir" || exit

    echo "Updating version: $PWD"
    jb_plugin_update_version 1>/dev/null 2>>"$log_file"

    cd ..
done
