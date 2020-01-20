# https://docs.docker.com/engine/examples/dotnetcore/

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-stage
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-stage /app/out .
ENTRYPOINT ["dotnet", "a-aspnetcore-rest.dll"]

# then run commands:
# docker build -t a-aspnetcore-rest .
# docker run -d -p 8081:80 --name bbb a-aspnetcore-rest

# open ...:8081/weatherforecast