FROM microsoft/aspnetcore-build:2.0

# Copy  client app and build Client
COPY . /app
WORKDIR /app

# RUN npm install --unsafe-perm -g webpack@3.9.1 --silent
# RUN npm install --silent
# RUN npm rebuild node-sass --force --silent
# RUN webpack

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
EXPOSE 80/tcp

ENV ASPNETCORE_ENVIRONMENT=Development
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh
