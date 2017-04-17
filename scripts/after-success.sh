#!/bin/bash
# dotnet publish -c Release ../src/Valorl.GTLibrary.Api/Valorl.GTLibrary.Api.csproj
source ./deploy-envs.sh
source ./docker-build.sh
if [ "$TRAVIS_BRANCH" == "master" ]; then source ./deploy.sh; fi
