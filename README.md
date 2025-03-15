# Blogify ✍️

Blogify is a blog platform application built with a clean architecture approach using .NET. The project is organized into multiple layers following domain-driven design principles.

## 🏗️ Project Structure

The solution consists of four main projects:

### 1. Blogify.Application 📱
Contains the application logic, DTOs, interfaces, and services.
- **DTOs**: Data Transfer Objects for various entities
- **Interfaces**: Service interfaces defining the application boundaries
- **Services**: Implementation of the application's core functionality

### 2. Blogify.Domain 🧠
Contains the core business logic, entities, and business rules.
- **Common**: Shared utilities like Result classes
- **Entities**: Domain models (BlogPost, Comment, Like, User)
- **Exceptions**: Custom domain exceptions

### 3. Blogify.Infrastructure 🔧
Contains implementation details, data access, and external services integration.
- **Data**: Database context and migrations
- **DependencyInjection**: Infrastructure module setup
- **Interfaces**: Repository interfaces
- **Middleware**: Application middleware like ExceptionMiddleware
- **Repositories**: Data access implementations

### 4. Blogify.WebAPI 🌐
The presentation layer exposing the application's functionality as a REST API.
- **Controllers**: API endpoints for different entities
- **Connected Services**: External service integrations
- **Properties**: Application configuration

## ✨ Features

- 🔐 User authentication and registration
- 📝 Blog post creation, reading, updating, and deletion
- 💬 Comments functionality
- 👍 Post likes system
- ⚠️ Exception handling and validation

## 🚀 Getting Started

### Prerequisites
- .NET 6.0 or later
- SQL Server

### Installation

1. Clone the repository
2. Navigate to the solution directory
3. Restore the NuGet packages
```
dotnet restore
```
4. Update the database connection string in `appsettings.json`
5. Apply the database migrations
```
dotnet ef database update --project Blogify.Infrastructure --startup-project Blogify.WebAPI
```
6. Run the application
```
dotnet run --project Blogify.WebAPI
```

## 🏛️ Architecture

Blogify follows a clean architecture pattern with:

- **Domain Layer** 📊: Core entities, exceptions, and business logic
- **Application Layer** ⚙️: Use cases, DTOs, and service interfaces
- **Infrastructure Layer** 🔌: Data access, external services, and implementations
- **Web API Layer** 🖥️: Controllers and presentation logic

## 📜 License

This project is licensed under the MIT License - see the LICENSE file for details.
