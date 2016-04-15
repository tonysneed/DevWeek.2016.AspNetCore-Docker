# Build: docker build -f mongoose.dockerfile -t tonysneed/mongoose .

# Option 1: Legacy Linking
 
# docker run -d -p 27017:27017 --name my-mongo -v mongo-data:/data/db mongo
# docker run -d -p 3000:3000 --link my-mongo:my-mongodb --name mongoose -v $(pwd):/var/www tonysneed/mongoose

# Option 2: Bridge Network
# NOTE: Before creating a container with -v $(pwd), you must cd into the app root directory

# docker network create --driver bridge my_network
# docker run -d --net=my_network --name my-mongodb -v mongo-data:/data/db mongo
# docker run -d -p 3000:3000 --net=my_network --name -v $(pwd):/var/www mongoose tonysneed/mongoose
 
FROM node:latest

MAINTAINER Anthony Sneed

ENV NODE_ENV=staging
ENV PORT=3000

COPY      . /var/www
WORKDIR   /var/www

RUN       npm install

EXPOSE $PORT

ENTRYPOINT ["node", "server.js"]