FROM node:9.6.1 as builder
RUN mkdir /app
WORKDIR /app
COPY package.json /app/package.json
RUN npm install --silent
RUN npm install webpack-cli -g --silent
COPY . /app
RUN npm run build

FROM microsoft/aspnetcore-build:2.0
COPY --from=builder /app /app
COPY . /app
WORKDIR /app

RUN dotnet restore
RUN dotnet publish -c Release -o out

RUN chmod a+x entrypoint.sh
RUN chmod a+x wait-for-it.sh


EXPOSE 80/tcp
ENTRYPOINT ["./entrypoint.sh"]
