#!/usr/bin/env bash

path=/home/db/dev/repo_g06

cd $path

find . -name '*.aux' -type f -delete
find . -name '*.out' -type f -delete
find . -name '*.tex' -type f -delete
find . -name '*.log' -type f -delete
