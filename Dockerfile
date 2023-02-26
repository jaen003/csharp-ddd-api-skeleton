FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine as base
WORKDIR /app
COPY . .
RUN dotnet restore; \
    dotnet publish -c Release -o Output --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
ARG USERNAME=backoffice
WORKDIR /home/$USERNAME
COPY --from=base /app/Output .
COPY .env .
RUN adduser -D $USERNAME
USER $USERNAME
EXPOSE 80
CMD ["dotnet", "Src.Api.dll"]