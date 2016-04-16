# Demo: Docker Compose

## PART A: Mongo Database

1. Pull down the official mongo image from DockerHub
  - See the mongo page for more info: https://hub.docker.com/_/mongo/
  - Execute the docker `run` command to pull the image and create a container
    + Supply the `--name` parameter to use with linking from other containers
    + Specify a volume on the Linux host for the database and log files

  ```
  docker run -d -p 27017:27017 --name my-mongo -v mongo-data:/data/db mongo
  ```

2. To interact with the dockerized mongo instance, install MongoChef
  - http://3t.io/mongochef
  - Open MongoChef and create a new connection
    + For the server, enter the docker machine IP address: 192.168.99.100
  - Click the Import button and select `customers.json` from the Data folder
    
## Part B: Mongoose Express

1. Install npm packages
  - Open a terminal and `cd` into the root folder
  - Restore npm packages: `npm install`
  
2. Take a look at config.js
  - Note that development uses the mongo container from OS X
  - But staging uses mongo via the host name: my-mongodb
  
3. Start the node server: `node server.js`
  - Open a browser to get all customers: `http://localhost:3000/customer/`
  - Then get a specific customer: `http://localhost:3000/customer/ALFKI`
  
4. Use Postman or similar client to issue POST, PUT and DELETE requests
  - Add a Content-Type header: `application/json`
  - POST Url: `http://localhost:3000/customer/`
  
  ```json
  {
    "CustomerId": "ABCDE",
    "CompanyName": "John Doe",
    "ContactName": "Jane Smith",
    "City": "London",
    "Country": "England"
  }
  ```

  - PUT Url: `http://localhost:3000/customer/ABCDE`
  
  ```json
  {
    "City": "Manchester"
  }
  ```
  
  - DELETE Url: `http://localhost:3000/customer/ABCDE`
  
5. Build docker image
  - Take a look at `mongoose.dockerfile`
    + Notice the `NODE_ENV` environment variable set to `staging`
  - Execute the following command from the Docker QuickStart Terminal
    + Make sure to `cd` into the MongooseExpress directory

  ```    
  docker build -f mongoose.dockerfile -t tonysneed/mongoose .
  ```
  
6. Create linked docker containers (legacy)

  ```
  docker run -d -p 27017:27017 --name my-mongo -v mongo-data:/data/db mongo
  docker run -d -p 3000:3000 --link my-mongo:my-mongodb --name mongoose -v $(pwd):/var/www tonysneed/mongoose
  ```
  
  - Browse to the following: `http://192.168.99.100:3000/customer/`
    + You should see JSON for all the customers
  
7. Create linked docker containers (bridge network)
  - First remove the existing containers (use `-f` flag to stop and remove)

  ```
  docker network create --driver bridge my_network
  docker run -d --net=my_network --name my-mongodb -v mongo-data:/data/db mongo
  docker run -d -p 3000:3000 --net=my_network --name -v $(pwd):/var/www mongoose tonysneed/mongoose
  ```
  
  - Note that the mongo container does not have a port mapping
    + It doesn't need to map an external port, because it is only used within the bridge network
    + This provides isolation and therefore greater security
  
## Part C: ASP.NET MVC Core App

1. Build a Docker image from the Dockerfile
  - Use the Docker QuickStart Terminal
  - `cd` into the app source directory
  - Build the docker image
    + Replace username with your DockerHub account name (or any name)
  - After building image, you can view the images: `docker images`
  
  ```
  docker build -f aspnetcore.dockerfile -t tonysneed/aspnetcore3 .
  ```

3. Create and run docker containers in a bridge network
  - Use `docker run` to create and start a container
  - Create a custom bridge network
  - Then use the `-v` flag to map the current working directory to the app folder
    + First `cd` into the app root directory
  
  ```
  docker network create --driver bridge my_network
  docker run -d --net=my_network --name my-mongodb -v mongo-data:/data/db mongo
  docker run -d --net=my_network --name mongoose -v $(pwd):/var/www tonysneed/mongoose
  docker run -d -p 5000:5000 --net=my_network --name aspnetcore3 -v $(pwd):/app tonysneed/aspnetcore3
  ```

4. Run the app from a browser
  - Run `docker-machine ip` to reveal the VM's IP address
  - Use the IP address to open the web app
    + `http://192.168.99.100:5000`
  - You should be able to update the web site and see the changes
    + You may need to restart the container for changes to take effect

## Part D: Docker Compose

1. Finally we get to use docker-compose for running multiple containers as a unit
  - Add a `docker-compose.yml` file
2. Add a `networks` section to declare a custom bridge network
  
  ```
  networks:
    aspnetcore-network:
      driver: bridge
  ```  

3. Add a `volumes` section to declare a named volume for the mongo container to use

  ```
  volumes:
    mongo-data:
  ```

4. Add a `services` section with one item for each container
  - Add `volumes`, `ports`, and `networks` to each service
  
  ```
  services:
        
    my-mongodb:
      image: mongo:latest
      volumes:
        - mongo-data:/data/db
      ports:
      - "27017:27017"
      networks:
        - aspnetcore-network
        
    mongoose:
      build:
        context: ./MongooseExpress
        dockerfile: mongoose.dockerfile
      volumes:
        - ./MongooseExpress:/var/www
      ports:
      - "3000:3000"
      networks:
        - aspnetcore-network

    aspnetcore3:
      build:
        context: ./DockerComposeDemo/src/DockerComposeDemo
        dockerfile: aspnetcore.dockerfile
      volumes:
        - ./DockerComposeDemo/src/DockerComposeDemo:/app
      ports:
      - "5000:5000"
      networks:
        - aspnetcore-network
  ```
  
5. Open the Docker QuickStart Terminal and use `docker-compose` commands
  - Get help to see the list of available commands
  - `cd` into the directory where the `docker-compose.yml` file is located
  - Bring up the containers with `docker-compose up`

6. The first time you will need to see the mongo database with `Data/customers.json`
  - You should be able to modify source files and see those reflected in the app
  - Using `inspect` can look at the mounts in each container to inspect the volume mapping
  - When finished bring down the containers with `docker-compose down`
  