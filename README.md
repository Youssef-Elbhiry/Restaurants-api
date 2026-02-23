# Restaurants API

This is a **.NET 9.0 web API** for managing restaurants, following the principles of **Clean Architecture**.

---

## Video Course

This solution is part of a video course.

You can find the course at [https://linktr.ee/fullstack_developer](https://linktr.ee/fullstack_developer).

The course covers the development of this .NET 9.0 web API for managing restaurants in detail, following **Clean Architecture principles**, CQRS with MediatR, FluentValidation, Serilog, and more.

---

## Project Structure

- `src/Restaurants.API`: The main API project. This is the entry point of the application.  
- `src/Restaurants.Application`: Contains the **application logic**. This layer is responsible for the application's behavior and policies.  
- `src/Restaurants.Domain`: Contains **enterprise logic and types**. This is the core layer of the application.  
- `src/Restaurants.Infrastructure`: Contains **infrastructure-related code** such as database and file system interactions. This layer supports the higher layers.  
- `tests/Restaurants.API.Tests`: Contains **unit and integration tests** for the API.  

---

## Packages and Libraries

This project uses several NuGet packages and libraries:

- **Serilog**: Provides flexible logging for the API.  
- **MediatR**: Implements the **CQRS pattern** for separating commands and queries.  
- **Entity Framework**: ORM for working with database data via domain objects.  
- **Azure Storage Account**: Handles **blob storage** for images, documents, and other unstructured data.  
- **Microsoft Identity**: Provides authentication, authorization, and user management.  
- **FluentValidation**: Validates request models with clear, reusable rules.  
- **AutoMapper**: Maps between domain and DTO objects.  

> Please refer to the official documentation of each package for more details.

---

## Getting Started

### Prerequisites

- .NET 9.0 SDK  
- Visual Studio 2022 or later  

### Building

Open `Restaurants.sln` in Visual Studio and **build the solution**.

### Running

Set `Restaurants.API` as the startup project in Visual Studio and start the application.

---

## API Endpoints

- `GET /api/restaurants`  
  Parameters: `searchPhrase`, `pageSize`, `pageNumber`, `sortBy`, `sortDirection`  
  Authorization: Bearer token  

- `GET /api/restaurants/{id}`  
  Parameters: `id`  
  Authorization: Bearer token  

- `GET /api/restaurants/{id}/dishes`  
  Parameters: `id`  
  Authorization: Bearer token  

- `DELETE /api/restaurants/{id}/dishes`  
  Parameters: `id`  

- `GET /api/restaurants/{id}/dishes/{dishId}`  
  Parameters: `id`, `dishId`  

- `DELETE /api/restaurants/{id}`  
  Parameters: `id`  
  Authorization: Bearer token  

- `POST /api/restaurants`  
  Body: JSON object with properties:  
  `Name`, `Description`, `Category`, `HasDelivery`, `ContactEmail`, `ContactNumber`, `City`, `Street`  
  Authorization: Bearer token  

---

## Features

- **Clean Architecture** implementation  
- **CQRS pattern** with **MediatR**  
- **AutoMapper** for DTO mapping  
- **Serilog** for logging  
- **FluentValidation** for input validation  
- **Global Exception Handling Middleware**  
- **Unit and Integration Testing** with **xUnit**  
- **Microsoft Identity** for authentication & authorization  
- **Pagination and sorting** of results  
- **CI/CD** pipeline for deployment to **Development** and **Production** environments  

---

## Deployment

This project is deployed using **GitHub Actions** CI/CD:

- Build → Publish → Upload artifact  
- Deploy to **Development** environment  
- Deploy to **Production** environment  

> Azure WebApps are used for hosting both environments.

---

## Testing

- Unit tests are implemented in `tests/Restaurants.API.Tests`  
- Integration tests verify API endpoints, database access, and middleware  
- Tests cover validation, authorization, and CRUD operations  

---

## License

This project is part of a **paid video course**. Please refer to the course link for usage instructions.
