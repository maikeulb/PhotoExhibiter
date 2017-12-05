FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /build

# Copy  client app and build Client
COPY ./package.json /build
COPY ./ClientApp ./ClientApp
COPY ./webpack.config.js .
RUN npm install
RUN npm install webpack -g
RUN webpack

# Copy csproj and restore dotnet
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build Dotnet
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 80/tcp
COPY --from=build-env /build/out .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "PhotoExhibiter.dll"]
