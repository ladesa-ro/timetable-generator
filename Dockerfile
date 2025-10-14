# =============================
# Etapa 1: SDK + ferramentas
# =============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS sdk

# Cria usuário happy (Debian-based SDK)
RUN groupadd -r happy && useradd -r -g happy -m -d /home/happy -s /bin/bash happy \
 && mkdir -p /home/happy/.dotnet/tools \
 && chown -R happy:happy /home/happy

USER happy
WORKDIR /src

# Variáveis de ambiente .NET
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_HOME=/home/happy/
ENV PATH="$PATH:/home/happy/.dotnet/tools"

# Instala CSharpier
RUN dotnet tool install --tool-path /home/happy/.dotnet/tools csharpier

# Stage devcontainer (opcional)
FROM sdk AS devcontainer

# =============================
# Etapa 2: Build
# =============================
FROM sdk AS build

WORKDIR /src
COPY . .

RUN dotnet restore ./Ladesa.TimetableGenerator/Ladesa.TimetableGenerator.slnx
RUN dotnet publish ./Ladesa.TimetableGenerator/Service/Service.csproj -c Release -o /app/publish /p:UseAppHost=false

# =============================
# Etapa 3: Runtime leve (Alpine)
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS timetable-generator-runtime

# Cria usuário happy no Alpine
RUN addgroup -S happy \
 && adduser -D -H -h /home/happy -s /bin/sh -G happy happy \
 && mkdir -p /home/happy \
 && chown -R happy:happy /home/happy

USER happy
WORKDIR /app

# Copia publicação do build
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Service.dll"]
