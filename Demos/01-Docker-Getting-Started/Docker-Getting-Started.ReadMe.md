# Demo: Getting Started with Docker

1. Install the Docker Toolbox
  - Go to docker.com and click Getting Started button
  - Install Docker Toolbox for your OS (Mac, Linux or Windows)

2. Enter some machine commands
  - `docker-machine ls`
  - `docker-machine ip`
  - `docker-machine env`

3. Run the **hello-world** container
  - Open the Docker QuickStart Terminal
  - Pull and run the container
    + `docker run hello-world`
  - Inspect the output
  - Show all containers
    + `docker ps -a`
  - Remove the container
    + `docker rm [container id: first three characters]`
  - List images
    + `docker images`
  - Remove the hello-world image
    + `rmi hello-world`
    
4. Pull down **microsoft/aspnet** images
  - Open Kitematic to search for microsoft/aspnet
  - Click View on DockerHub
  - Pull down the following images:
  
    ```
    microsoft/aspnet:rc1-update1    
    microsoft/aspnet:rc1-update1-coreclr
    ```

  - List images
    + `docker images`