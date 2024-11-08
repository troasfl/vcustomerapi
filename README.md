# Troas.Customer.Api

## Overview

Troas.Customer.Api is a .NET 8 web API project that provides CRUD operations for managing customer information. This application leverages Entity Framework Core for data access, PostgreSQL as the database, and follows Clean Architecture principles. It organizes the codebase into distinct layers for scalability, maintainability, and separation of concerns.

## Project Architecture

### 1. Troas.Customer.Domain

The heart of the application, holds the domain entities. It represents the core of your application and remains independent of any external frameworks.

### 2. Troas.Customer.Application

This encapsulates application-specific business rules, use cases, and application services. It acts as a mediator between the domain layer and the infrastructure layer.

### 3. Troas.Customer.Infrastructure

This contains implementation details that are external to the application. It includes data access, external services, and other infrastructure concerns. The project deals with data storage and retrieval, using technologies such as Entity Framework Core to interact with the database.

### 4. Troas.Customer.Api

This serves as the entry point for the Web API application. It utilizes the Clean Architecture principles to handle incoming HTTP requests and coordinate actions across different layers.

## Features

1. **.NET Core 8:**
   - Utilizes the latest version of .NET Core for enhanced performance and features.

2. **ModelState Validation:**
   - Integrates ModelState Validation for robust input validation and improved request handling.

3. **Application Insights:**
   - Uses Application Insights for Metrics Collection

4. **Entity Framework Core:**
   - Leverages Entity Framework Core for data access and database management.

5. **GELF for Graylogs:**
   - Incorporates GELF for structured logging and easy log analysis.

6. **Dependency Injection:**
    - Utilizes the built-in dependency injection in .NET Core for managing application components.

## Prerequisites
1. [.NET 8 SDK](https://dotnet.microsoft.com/download) installed.
2. DockerHub account
3. GitHub account and Actions for CI/CD
2. Kubernetes account online with any supported cloud provider (DigitalOcean used)
3. PostgreSQL installed and running (DigitalOcean used).
4. Application Insights Connection String
5. Jaeger (View trace here http://localhost:16686). You can create a docker container running it using: 
   ```bash
    docker run --name jaeger -p 13133:13133 -p 16686:16686 -p 4317:4317 -d --restart=unless-stopped jaegertracing/opentelemetry-all-in-one
    ```
   
## Continuous Integration / Continuous Deployment (CI/CD)

This project is configured for CI/CD. The workflow is triggered on each push to the master branch, running build and test tasks.

### Docker

Run the application in Docker containers using k8s:

1. **Build and run the Docker containers:**



2. **Access the Web API:**

   - The API will be accessible at [http://localhost:5182](http://localhost:5182).
   


### CI/CD Pipeline

The CI/CD pipeline is automatically triggered on each push to the main branch. It includes the following steps:

1. **Build:**
   - Builds the application, ensuring the code compiles successfully.

2. **Test:**
   - Runs automated tests to verify the integrity of the codebase.

3. **Publish:**
   - Publishes the application to docker hub, preparing it for deployment.

4. **Deploy:**
   - Deploys the application to the k8s cluster environment.


Feel free to customize the CI/CD workflow in the `.github/workflows` directory based on your deployment needs.

## Usage

Set the *.Api as startup in your IDE of choice and run/debug it.


## Contributing

1. Fork the project.
2. Create a new branch: `git checkout -b feature/my-feature`.
3. Make changes and commit them: `git commit -m 'Add new feature'`.
4. Push to the branch: `git push origin feature/my-feature`.
5. Open a pull request.
