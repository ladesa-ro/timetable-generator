#!/usr/bin/env bash

here=$(pwd)
img=$(podman build . --target tool-csharpier -q)

podman run --rm -v ${here}:/repo -w /repo ${img} format .