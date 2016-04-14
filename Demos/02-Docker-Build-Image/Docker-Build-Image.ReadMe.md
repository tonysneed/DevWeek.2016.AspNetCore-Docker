# Demo: Build a Docker Image

## Part A: Create an empty ASP.NET Core web app using Yeoman and VS Code

1. Install **Visual Studio Code** with C# and Docker extensions
  - Go to code.visualstudio.com
    + Download and install VS Code
  - From within VS Code, type Cmd+P, then ext
    + Install the C# extension
    + Install the extension for Dockerfile and Docker Compose support

2. Install Nodejs, Yeoman, AspNet generator
  - Go to nodejs.org to download the latest stable version
  - Install Yeoman using npm: `npm install -g yo`
  - Install **aspnet** generator: `npm install -g generator-aspnet`

3. Run the **aspnet** Yeoman generator
  - Open a Terminal in the Before Folder
  - Run the generator for aspnet: `yo aspnet`
  - Select Empty Application
  - Enter a name such as AspNetHelloDocker
  - Open project in Visual Studio Code from the Terminal
    + `cd AspNetHelloDocker`
    + `code .`
  - Append the following argument to the "web" command in project.json
    + `--server.urls http://*:5000`
  - Edit the Startup.Configure method
    + Add an IHostingEnvironment parameters
    + Update the output message to include the environment name
    
    ```csharp
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync($"Hello ASP.NET Core on {env.EnvironmentName}!");
        });
    }
    ```

## Part B: Deploy the web app using Docker
    
1. Edit the Dockerfile included with the project
  - Append `-coreclr` to the `FROM` directive
  - Remove the two `RUN` directives
  - Add a `MAINTAINER` directive
  - Add an environment variable: `ENV ASPNET_ENV="Staging"`
  
2. Build the image using Docker client
  - Use the Docker QuickStart Terminal
  - `cd` into the app source directory
  - Build the docker image
    + Replace username with your DockerHub account name (or any name)
    + The --file argument is optional
  - After building image, you can view the images
    + `docker images`
  
    ```
    docker build --tag <username>/aspnetcore1 -- file Dockerfile .
    ```

3. Create and run a docker container
  - Use `docker run` to create and start a container
  
    ```
    docker run -d -p 5000:5000 --name web1 tonysneed/aspnetcore1
    ```

4. Run the app from a browser
  - Run `docker-machine ip` to reveal the VM's IP address
  - Use the IP address to open the web app
    + `http://192.168.99.100:5000`

5. Start an additional container
  - Replace the port mapping: `-p 5001:5000`
  - Run the web app: `http://192.168.99.100:5001` 
  - See all the running containers: `docker ps`

