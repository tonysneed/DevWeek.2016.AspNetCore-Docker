# Build: docker build -f aspnetcore.dockerfile -t tonysneed/aspnetcore3 .

# Option 1: Legacy Linking
 
# docker run -d -p 27017:27017 --name my-mongo -v mongo-data:/data/db mongo
# docker run -d -p 3000:3000 --link my-mongo:my-mongodb --name mongoose -v $(pwd):/var/www tonysneed/mongoose
# docker run -d -p 5000:5000 --link mongoose:mongoose --name aspnetcore3 -v $(pwd):/app tonysneed/aspnetcore3

# Option 2: Bridge Network
# NOTE: Before creating a container with -v $(pwd), you must cd into the app root directory

# docker network create --driver bridge my_network
# docker run -d --net=my_network --name my-mongodb -v mongo-data:/data/db mongo
# docker run -d --net=my_network --name mongoose -v $(pwd):/var/www tonysneed/mongoose
# docker run -d -p 5000:5000 --net=my_network --name aspnetcore3 -v $(pwd):/app tonysneed/aspnetcore3

FROM microsoft/aspnet:1.0.0-rc1-update1-coreclr

ENV ASPNET_ENV=staging
ENV PORT=5000

COPY . /app
WORKDIR /app

RUN ["dnu", "restore"]

EXPOSE 5000/tcp

ENTRYPOINT ["dnx", "-p", "project.json", "web"]
