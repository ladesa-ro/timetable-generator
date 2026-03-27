#!/usr/bin/env bash

set -xeo pipefail

kubectl apply -f ./secrets/timetable-generator.yaml
kubectl apply -f ./deployments/timetable-generator.yaml
kubectl apply -f ./services/timetable-generator.yaml

# Restart deployments
kubectl rollout restart \
  deployment.apps/ladesa-ro-timetable-api \
  deployment.apps/ladesa-ro-timetable-worker \
  --namespace ladesa-ro-development

# Wait for rollouts to complete
kubectl rollout status \
  deployment/ladesa-ro-timetable-api \
  --namespace ladesa-ro-development \
  --timeout=300s

kubectl rollout status \
  deployment/ladesa-ro-timetable-worker \
  --namespace ladesa-ro-development \
  --timeout=300s
