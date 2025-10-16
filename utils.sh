#!/usr/bin/env bash
set -e

function generate_protos() {
  PROTO_SRC="./vendors/ladesa-ro/ladesa-protobufs/protos/timetable-generator-v1"
  OUT_DIR="./Ladesa.TimetableGenerator/Service/Infrastructure/Protos"
  TARGET_NAMESPACE="Ladesa.TimetableGenerator.Service.Infrastructure.Protos"

  rm -rf "$OUT_DIR"
  mkdir -p "$OUT_DIR"

  echo "🔹 Gerando C# DTOs..."
  protoc \
    --proto_path="$PROTO_SRC" \
    --csharp_out="$OUT_DIR" \
    $(find "$PROTO_SRC" -name "*.proto")

  echo "🔧 Ajustando namespaces nos arquivos gerados..."
  find "$OUT_DIR" -type f -name "*.cs" | while read -r file; do
    sed -i -E "s|^namespace .*$|namespace $TARGET_NAMESPACE {|" "$file"
  done

  csharpier format "$OUT_DIR"
  echo "✅ Geração concluída e namespaces ajustados: $OUT_DIR"
}

function format() {
  csharpier format .
}

function sync_submodules() {
  echo "🔄 Sincronizando e atualizando submodules..."
  git submodule sync
  git submodule update --init --recursive
  echo "✅ Submodules sincronizados e atualizados."
}

function print_usage() {
  echo "Uso: $0 <comando>"
  echo "Comandos disponíveis:"
  echo "  generate_protos   Gera os arquivos C# a partir dos arquivos .proto"
  echo "  format            Formata o código usando csharpier"
  echo "  sync_submodules   Sincroniza e atualiza os git submodules"
  echo "  help              Mostra esta mensagem de ajuda"
}

# Verifica argumento
case "$1" in
  generate_protos)
    generate_protos
    ;;
  format)
    format
    ;;
  sync_submodules)
    sync_submodules
    ;;
  help|"" )
    print_usage
    ;;
  *)
    echo "Comando desconhecido: $1"
    print_usage
    exit 1
    ;;
esac
