#!/usr/bin/env bash

set -e

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
TARGET="$SCRIPT_DIR/bin/Debug/net10.0"

if [ -d "$TARGET" ]; then
    cd "$TARGET"
    ./yoma-install
else
    echo "Folder not found: $TARGET"
    exit 1
fi
