FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /workspaces/backoffice
COPY Backoffice.sln .config Directory.Packages.props .
COPY Src/Api/Api.csproj Src/Api/Api.csproj
COPY Src/Core/Core.csproj Src/Core/Core.csproj
COPY Tests/Tests.csproj Tests/Tests.csproj
RUN apt-get update \
    && apt-get install -y --no-install-recommends make \
    && dotnet restore \
    && dotnet tool restore \
    && apt-get autoremove -y \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/*