# =============================
# Etapa 1: Base Debian
# =============================
FROM docker.io/debian:bookworm-slim AS os-slim

# =============================
# Etapa 2: Base Debian para SDK
# =============================
FROM os-slim AS os-sdk-base

ENV DEBIAN_FRONTEND=noninteractive
ENV LANG=en_US.UTF-8
ENV LC_ALL=en_US.UTF-8

RUN apt-get update && apt-get install -y --no-install-recommends \
  curl ca-certificates libc6 libgcc1 libgssapi-krb5-2 libicu72 libssl3 libstdc++6 zlib1g \
  git vim zsh less procps locales \
  && echo "en_US.UTF-8 UTF-8" > /etc/locale.gen && locale-gen \
  && rm -rf /var/lib/apt/lists/*

# =============================
# Etapa 3: Base Debian + .NET SDK (para build e dev)
# =============================
FROM os-sdk-base AS sdk-base

WORKDIR /src

# =============================
# Configurações do .NET
# =============================
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

ENV DOTNET_CHANNEL=9.0
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="$PATH:/usr/share/dotnet"

# Diretório compartilhado para cache e ferramentas .NET (gravável por todos)
ENV DOTNET_CLI_HOME=/usr/share/dotnet/home
RUN mkdir -p $DOTNET_CLI_HOME && chmod -R 777 $DOTNET_CLI_HOME

# Instala o SDK do .NET
RUN curl -fsSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh \
  && chmod +x dotnet-install.sh \
  && ./dotnet-install.sh --install-dir /usr/share/dotnet --channel $DOTNET_CHANNEL \
  && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
  && rm dotnet-install.sh

# =============================
# Instalação global de ferramentas .NET (disponíveis para todos os usuários)
# =============================
ENV DOTNET_TOOLS_PATH=/usr/share/dotnet-tools
RUN mkdir -p $DOTNET_TOOLS_PATH \
  && dotnet tool install --tool-path $DOTNET_TOOLS_PATH csharpier

ENV PATH="$PATH:$DOTNET_TOOLS_PATH"

RUN apt-get update && apt-get install -y --no-install-recommends \
  protobuf-compiler \
  && rm -rf /var/lib/apt/lists/*

# =============================
# Etapa 4: Devcontainer (ambiente completo)
# =============================
FROM sdk-base AS devcontainer

ARG USERNAME=dev
ARG USER_UID=1000
ARG USER_GID=1000

ENV HOME=/home/${USERNAME}
ENV SHELL=/usr/bin/zsh

# Cria o usuário de desenvolvimento não-root
RUN groupadd --gid $USER_GID ${USERNAME} && \
  useradd --uid $USER_UID --gid $USER_GID -m -s /usr/bin/zsh ${USERNAME} && \
  chown -R ${USERNAME}:${USERNAME} /src \
  && mkdir -p ${HOME}/.dotnet && chown -R ${USERNAME}:${USERNAME} $HOME/.dotnet

# Define home .NET do usuário dev
ENV DOTNET_CLI_HOME=$HOME/.dotnet
ENV PATH="$PATH:/usr/share/dotnet-tools"

USER 1000:1000
RUN sh -c "$(curl -fsSL https://raw.githubusercontent.com/ohmyzsh/ohmyzsh/master/tools/install.sh)"
RUN sed -i 's/^ZSH_THEME=.*/ZSH_THEME="josh"/' ${HOME}/.zshrc

USER ${USERNAME}
WORKDIR /src

# =============================
# Etapa 5: Build da aplicação
# =============================
FROM sdk-base AS build

WORKDIR /src

# Copia apenas os arquivos de projeto para melhor aproveitamento de cache
COPY ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.v1.slnx ./Ladesa.TimetableGenerator.v1/
COPY ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Domain/*.csproj ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Domain/
COPY ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Application/*.csproj ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Application/
COPY ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Infrastructure.RabbitMq/*.csproj ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Infrastructure.RabbitMq/
COPY ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Infrastructure.Solver/*.csproj ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Infrastructure.Solver/
COPY ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Server.Api/*.csproj ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Server.Api/
COPY ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Server.Workers.Generator/*.csproj ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Server.Workers.Generator/
COPY ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Domain.Test/*.csproj ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Domain.Test/

RUN dotnet restore ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.v1.slnx

# Copia o código completo
COPY . .

# Build e publish - API
RUN dotnet publish ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Server.Api/Ladesa.TimetableGenerator.Server.Api.csproj \
  -c Release -o /app/publish-api /p:UseAppHost=false

# Build e publish - Worker
RUN dotnet publish ./Ladesa.TimetableGenerator.v1/Ladesa.TimetableGenerator.Server.Workers.Generator/Ladesa.TimetableGenerator.Server.Workers.Generator.csproj \
  -c Release -o /app/publish-worker /p:UseAppHost=false

# =============================
# Etapa 5a: Runtime da API
# =============================
FROM os-slim AS timetable-api-runtime

ENV DOTNET_CHANNEL=9.0
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="$PATH:/usr/share/dotnet"
ENV ASPNETCORE_URLS=http://+:8080

RUN apt-get update && apt-get install -y --no-install-recommends \
  curl ca-certificates libc6 libgcc1 libgssapi-krb5-2 libicu72 libssl3 libstdc++6 zlib1g \
  && rm -rf /var/lib/apt/lists/*

RUN curl -fsSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh \
  && chmod +x dotnet-install.sh \
  && ./dotnet-install.sh --runtime aspnetcore --install-dir /usr/share/dotnet --channel $DOTNET_CHANNEL \
  && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
  && rm dotnet-install.sh

ARG APP_USER=appuser
RUN addgroup --system $APP_USER && adduser --system --ingroup $APP_USER $APP_USER

WORKDIR /app
COPY --from=build /app/publish-api .

USER $APP_USER

ENTRYPOINT ["dotnet", "Ladesa.TimetableGenerator.Server.Api.dll"]

# =============================
# Etapa 5b: Runtime do Worker
# =============================
FROM os-slim AS timetable-worker-runtime

ENV DOTNET_CHANNEL=9.0
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="$PATH:/usr/share/dotnet"

RUN apt-get update && apt-get install -y --no-install-recommends \
  curl ca-certificates libc6 libgcc1 libgssapi-krb5-2 libicu72 libssl3 libstdc++6 zlib1g \
  && rm -rf /var/lib/apt/lists/*

RUN curl -fsSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh \
  && chmod +x dotnet-install.sh \
  && ./dotnet-install.sh --runtime dotnet --install-dir /usr/share/dotnet --channel $DOTNET_CHANNEL \
  && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
  && rm dotnet-install.sh

ARG APP_USER=appuser
RUN addgroup --system $APP_USER && adduser --system --ingroup $APP_USER $APP_USER

WORKDIR /app
COPY --from=build /app/publish-worker .

USER $APP_USER

ENTRYPOINT ["dotnet", "Ladesa.TimetableGenerator.Server.Workers.Generator.dll"]
