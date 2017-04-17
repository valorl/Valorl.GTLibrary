FROM microsoft/dotnet:latest
COPY api /app
WORKDIR /app
RUN ["dotnet", "restore"]
WORKDIR /app/Valorl.GTLibrary.Api
RUN ["dotnet", "build"]
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS https://*:5000
ENTRYPOINT ["dotnet", "run", "--server.urls", "http://*:5000"]
