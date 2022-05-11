#!/bin/bash

NAME=ip_api

docker rm -f $NAME
docker build --no-cache -t $NAME .
docker run -it --name $NAME $NAME

