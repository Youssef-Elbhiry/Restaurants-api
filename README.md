# Restaurants API

This is a **simple ASP.NET 9.0 Web API** for managing restaurants.  
The project follows **Clean Architecture principles** and is divided into four layers:

- **Presentation / API**: Handles HTTP requests and responses.  
- **Application**: Contains business logic and CQRS commands/queries.  
- **Domain**: Contains core entities and business rules.  
- **Infrastructure**: Handles database access, file storage, and other external services.  

The project uses **SQL Server, Entity Framework, LINQ**, and other technologies for building a clean, maintainable API.

---

## Project Structure

- `src/Restaurants.API` → The main API project, entry point of the application.  
- `src/Restaurants.Application` → Application logic and CQRS handlers.  
- `src/Restaurants.Domain` → Core entities and business rules.  
- `src/Restaurants.Infrastructure` → Database, storage, and external dependencies.  
- `tests/Restaurants.API.Tests` → Unit and integration tests for the API.

---

## Packages and Libraries

- **Serilog** → Flexible logging  
- **MediatR** → Implements CQRS pattern (commands and queries separation)  
- **Entity Framework Core** → ORM for database access  
- **FluentValidation** → Request validation  
- **AutoMapper** → Maps between entities and DTOs  
- **Microsoft Identity** → Handles authentication and authorization  

---

## API Endpoints

### Restaurants

<img width="1813" height="500" alt="image" src="https://github.com/user-attachments/assets/e296963a-73d2-401c-b773-149198aaee43" />
 

### Dishes
<img width="1837" height="365" alt="image" src="https://github.com/user-attachments/assets/da2d530e-ba71-44bb-b752-bf2e7ccf9d6f" />


### Identity

<img width="1832" height="922" alt="image" src="https://github.com/user-attachments/assets/37c63054-161e-4388-a0ed-9fde34bef2c0" />

---

## Features

- Clean Architecture with **four layers**: Presentation, Application, Domain, Infrastructure  
- **CQRS pattern** using MediatR  
- **Entity Framework Core** with LINQ for database operations  
- **Serilog** for structured logging  
- **FluentValidation** for request validation  
- **AutoMapper** for mapping DTOs  
- **Microsoft Identity** for authentication & authorization  
- Unit and integration testing with **xUnit**  
- Pagination, sorting, and filtering for endpoints  
- Global exception handling middleware  

---

## Getting Started

### Prerequisites

- .NET 9.0 SDK  
- Visual Studio 2022 or later  
- SQL Server or LocalDB  

### Build and Run

1. Open `Restaurants.sln` in Visual Studio.  
2. Set `Restaurants.API` as the startup project.  
3. Build the solution and run the API.  
4. The API will be accessible at `https://localhost:{port}`.

---

## Testing

- Unit tests and integration tests are in `tests/Restaurants.API.Tests`  
- Tests cover CRUD operations, validation, authentication, and authorization  

---

## Deployment

- Can be deployed to **Azure Web Apps** or any hosting supporting .NET 9.0.  
- CI/CD can be configured using **GitHub Actions** for automated build and deployment.
