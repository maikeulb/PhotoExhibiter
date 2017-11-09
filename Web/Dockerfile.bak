FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /Web

# Copy csproj and restore as distinct layers
COPY Web.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -o /publish

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "Web.dll"]
