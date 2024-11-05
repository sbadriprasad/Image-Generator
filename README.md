# Image-Generator Solution
## Overview
The **Image-Generator** solution is built with .NET 8 and consists of two main projects:
- **ImageGenerator.API**: This project includes the REST API, Postgres SQL database integration, repository, services, and controllers.
- **ImageGenerator.Test**: This project contains unit tests for the service and controller layers, using mocking and the In-Memory Database feature of Entity Framework Core.

The solution is containerized with Docker and includes a `docker-compose` file for easy setup and deployment.
## Features
- REST API for saving random animal pictures (cats, dogs, bears) to a database.
- REST API for fetching the last saved picture of an animal.
- Automated unit tests that mock services and test both service and controller layers.
- Containerized application for streamlined deployment.
## Prerequisites
- .NET 8 SDK installed on your system.
- Docker Desktop installed and running.
- PostgreSQL client for database access (optional for direct access).
- Make sure local dev certificate is avaliable & trusted
  
## How to Run the Solution
### Step 1: Clone the Repository
Clone the repository or extract the ZIP file containing the solution.

### Step 2: Build and Run Using Docker Compose
Ensure Docker Desktop is running then use the following commands to build and run the containers:
~~~
docker-compose up --build
~~~
This command will build API Project & Start the PostgreSQL database container.
Also it will run teh API contaier and expose it on https://localhost:5001

### Step 3: Test teh Endpoints
Open browser and access the below swagger url
~~~
https://localhost:5001
~~~
You can also use curl command or postman tool to acces the REST API

### Step 4: To Stop the container
~~~
docker-compose down
~~~
