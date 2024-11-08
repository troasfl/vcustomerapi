# Use a specific version of the .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Troas.Customer.Api/Troas.Customer.Api.csproj", "Troas.Customer.Api/"]
RUN dotnet restore "Troas.Customer.Api/Troas.Customer.Api.csproj"

# Copy the rest of the code
COPY . .

# Set the working directory to the API project
WORKDIR "/src/Troas.Customer.Api"

# Build the project
RUN dotnet build -c Release -o /app

# Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Troas.Customer.Api.dll"]
