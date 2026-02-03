#!/usr/bin/env bash

set -xeo pipefail

kubectl apply -f ./deployments/timetable-generator.yaml
kubectl apply -f ./services/timetable-generator.yaml

# Reinicia o deployment
kubectl rollout restart \
  deployment.apps/timetable-generator \
  --namespace ladesa

# Aguarda o rollout finalizar
kubectl rollout status \
  deployment/timetable-generator \
  --namespace ladesa
  --timeout=300s
