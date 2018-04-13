FROM microsoft/aspnetcore-build:2.0

COPY . /app
WORKDIR /app

RUN dotnet restore
RUN dotnet publish -c Release -o out

RUN chmod a+x entrypoint.sh
RUN chmod a+x wait-for-it.sh

ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80/tcp
ENTRYPOINT ["./entrypoint.sh"]
