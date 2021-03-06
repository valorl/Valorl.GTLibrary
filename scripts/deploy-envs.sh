#!/bin/bash

# set environment variables used in deploy.sh and AWS task-definition.json:
export IMAGE_NAME=valorl-gtlibrary-backend
export IMAGE_VERSION=${TRAVIS_COMMIT::6}-$TRAVIS_BUILD_NUMBER

export AWS_DEFAULT_REGION=eu-west-1
export AWS_ECS_CLUSTER_NAME=default
export AWS_VIRTUAL_HOST=api.gtl.enartee.online

# set any sensitive information in travis-ci encrypted project settings:
# required: AWS_ACCOUNT_NUMBER, AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY
# optional: SERVICESTACK_LICENSE
