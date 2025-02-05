FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS base
WORKDIR /src
COPY . .
RUN dotnet restore && \
    dotnet publish Src/Api/Api.csproj -c Release -o /app/Output --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
ARG APP_USER=appuser
ARG APP_GROUP=appgroup
ARG APP_WORKDIR=/app
WORKDIR $APP_WORKDIR
COPY --from=base /app/Output .
RUN addgroup -S $APP_GROUP && \
    adduser -S $APP_USER -G $APP_GROUP && \
    chown -R $APP_USER:$APP_GROUP $APP_WORKDIR
USER $APP_USER
EXPOSE 80
ENTRYPOINT ["dotnet", "Src.Api.dll"]