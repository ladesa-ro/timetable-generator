# =============================
# Etapa 1: SDK + ferramentas
# =============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS sdk

WORKDIR /src

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_HOME=/root/
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet tool install --tool-path /root/.dotnet/tools csharpier

# Stage devcontainer (opcional)
FROM sdk AS devcontainer

# =============================
# Etapa 2: Build
# =============================
FROM sdk AS build

USER happy

WORKDIR /src
COPY . .

RUN dotnet restore ./Ladesa.TimetableGenerator/Ladesa.TimetableGenerator.slnx
RUN dotnet publish ./Ladesa.TimetableGenerator/Service/Service.csproj -c Release -o /app/publish /p:UseAppHost=false

# =============================
# Etapa 3: Runtime leve (Alpine)
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS timetable-generator-runtime

USER happy
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Service.dll"]
