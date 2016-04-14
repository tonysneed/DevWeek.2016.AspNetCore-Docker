# Demo: Map to Source Code

1. Build a Docker image from the Dockerfile
  - Use the Docker QuickStart Terminal
  - `cd` into the app source directory
  - Build the docker image
    + Replace username with your DockerHub account name (or any name)
    + The --file argument is optional
  - After building image, you can view the images
    + `docker images`
  
    ```
    docker build --tag <username>/aspnetcore2 -- file Dockerfile .
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
  
