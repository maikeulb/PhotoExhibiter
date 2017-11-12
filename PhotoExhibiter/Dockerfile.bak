FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /Web

# Copy csproj and restore as distinct layers
COPY Web.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /Web
COPY --from=build-env /Web/out .
ENTRYPOINT ["dotnet", "Web.dll"]
