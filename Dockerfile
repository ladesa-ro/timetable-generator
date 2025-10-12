FROM mcr.microsoft.com/dotnet/sdk:9.0 AS tool-csharpier
RUN dotnet tool install csharpier --tool-path /bin 
ENTRYPOINT [ "csharpier" ]


FROM docker.io/alpine AS timetable-generator-runtime

ENTRYPOINT ["sleep", "infinity"]
