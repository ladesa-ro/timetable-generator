#!/usr/bin/env bash

set -xeo pipefail

# Aplica secrets e deployment
kubectl apply -f ./secrets/timetable-generator.yaml
kubectl apply -f ./deployments/timetable-generator.yaml

# Reinicia o deployment
kubectl rollout restart \
  deployment.apps/ladesa-ro-timetable-generator \
  --namespace ladesa-ro-development

# Aguarda o rollout finalizar
kubectl rollout status \
  deployment/ladesa-ro-timetable-generator \
  --namespace ladesa-ro-development
