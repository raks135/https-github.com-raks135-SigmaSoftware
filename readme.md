# Candidate Management API

This is a web application built with ASP.NET Core, which provides an API to store and manage job candidate contact information. 
The API supports creating and updating candidate information in a scalable and maintainable way. 
The application is designed with future growth in mind, capable of handling large volumes of data as the number of candidates 
increases.

# Solution: SigmaSoftware
SigmaSoftware (Solution)
│
├── Core
│   ├── SigmaSoftware.Application
│   └── SigmaSoftware.Domain
│
├── Infrastructure
│   ├── SigmaSoftware.Infrastructure
│   └── SigmaSoftware.Shared
│
├── Server
│   └── SigmaSoftware.API
│
└── Tests
    └── SigmaSoftware.Tests
        ├── Dependencies
        ├── CandidateServiceTests.cs
        ├── GlobalUsings.cs
        └── MemoryCache.cs

# Core
    ## SigmaSoftware.Application: Likely handles application-level logic and business operations.
    ## SigmaSoftware.Domain: Contains domain models and business rules.
# Infrastructure

    ## SigmaSoftware.Infrastructure: Responsible for data access, external services, and infrastructure-related concerns.
    ## SigmaSoftware.Shared: Common libraries or utilities shared across the solution.
# Server

    ## SigmaSoftware.API: The API project that exposes the endpoints and handles requests from the client applications.
# Tests

    ## SigmaSoftware.Tests: Contains unit and integration tests for the application.

## Features

- **Create/Update Candidate**: API to create or update candidate contact details.
- **Validation**: Candidate data is validated using a custom DTO validator before being processed.
- **Logging**: Integrated logging to track operations and errors.
- **Caching**: In-memory caching has been implemented to optimize repeated lookups.
- **Scalability**: The application is designed to handle a large volume of candidate data with future growth in mind.

## Requirements

- .NET 8 SDK
- SQL Server or PostgreSQL for database storage
- Visual Studio or any preferred .NET development environment

## Getting Started

### 1. Clone the Repository

Clone the repository to your local machine using the following command:

```bash
git clone https://github.com/raks135/https-github.com-raks135-SigmaSoftware
