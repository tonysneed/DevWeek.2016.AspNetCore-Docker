# docker build --tag tonysneed/aspnetcore2 -- file Dockerfile .

# docker run -d -p 5000:5000 --name web2 -v $(pwd):/app tonysneed/aspnetcore2

FROM microsoft/aspnet:1.0.0-rc1-update1-coreclr

MAINTAINER Anthony Sneed

COPY . /app

WORKDIR /app

RUN ["dnu", "restore"]

ENV ASPNET_ENV="Development"

EXPOSE 5000/tcp

ENTRYPOINT ["dnx", "-p", "project.json", "web"]
