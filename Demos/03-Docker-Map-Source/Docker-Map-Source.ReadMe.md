# Demo: Map to Source Code

## Part A: Mount a Volume to Map Source Code

1. Build a Docker image from the Dockerfile
  - Use the Docker QuickStart Terminal
  - `cd` into the app source directory
  - Build the docker image
    + Replace username with your DockerHub account name (or any name)
    + The --file argument is optional
  - After building image, you can view the images
    + `docker images`
  
    ```
    docker build --tag tonysneed/aspnetcore2 -- file Dockerfile .
    ```

3. Create and run a docker container
  - Use `docker run` to create and start a container
  - Use the `-v` flag to map the current working directory to the app folder
  
    ```
    docker run -d -p 5000:5000 --name web2 -v $(pwd):/app tonysneed/aspnetcore2
    ```

4. Run the app from a browser
  - Run `docker-machine ip` to reveal the VM's IP address
  - Use the IP address to open the web app
    + `http://192.168.99.100:5000/hello.html`

5. Now with the container still running, edit the file **hello.html** under wwwroot
  - Add "from Docker" to the end of the greeting
  - Simply *refresh the browser* to view the updated text!
  
  
## Part B: Monitoring File Changes with Dnx-Watch

1. In this part we've refactored the project to use an MVC controller.
  - Add a dependency for Mvc, call AddMvc and UseMvc in the Startup class
  - This is so that we can use `dnx-watch` to restart the app when changes are detected
  - Add a Home controller with an Index action that returns formatted text
  - Inject IHostingEnvironment to get the environment name
  
2. Add a Dockerfile which is based on microsoft/aspnet:1.0.0-rc1-update1
  - Note that we must depend on the Mono-based image, rather than on Core CLR,
    in order for dnx-watch to work
  - We need to set some environment variables for location of packages and commands
  - We install the dnx-watch command, which monitors file changes
  
3. Build the image, then create and run a container based on the image

4. Run the app from a browser
  - Run `docker-machine ip` to reveal the VM's IP address
  - Use the IP address to open the web app
    + `http://192.168.99.100:5000`

5. Now with the container still running, edit the **HomeController** under Controllers
  - Change the text returned from the Index action
  - Then *refresh the browser* to view the updated text!

