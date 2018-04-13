#!/bin/bash

set -e
run_cmd="dotnet /app/out/PhotoExhibiter.dll"

echo waiting for db
./wait-for-it.sh db:3306

until dotnet ef database update; do
>&2 echo "MySQL is starting up"
sleep 1
done

>&2 echo "MySQL is up - executing command"
exec $run_cmd
