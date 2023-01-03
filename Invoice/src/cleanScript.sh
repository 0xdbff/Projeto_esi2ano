#!/usr/bin/env bash

path=/home/db/dev/repo_g06

cd $path

find . -name $1'*.aux' -type f -delete
find . -name $1'*.out' -type f -delete
find . -name $1'*.tex' -type f -delete
find . -name $1'*.log' -type f -delete

