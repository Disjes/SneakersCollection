## Configuration File

Copy the following configuration and replace the placeholder values with your local settings:

```json
{
  "Swagger": {
    "ApiTitle": "Sneakers Collection API",
    "ApiVersion": "v1",
    "AuthorizationUrl": "https://localhost:<ISPort>/connect/authorize",
    "TokenUrl": "https://localhost:<ISPort>/connect/token",
    "Audience": "sneakerapi",
    "ClientId": "m2m.client",
    "ClientSecret": "82124972-4616-4d29-9c16-ccd29bc5d157",
    "RedirectUrl": "https://localhost:<ISPort>/swagger/oauth2-redirect.html",
    "OpenIdConnectUrl": "https://localhost:<isPort>/.well-known/openid-configuration",
    "Scope1": "sneakerapi.read",
    "Scope2": "sneakerapi.write"
  },
  "Jwt": {
    "Authority": "https://localhost:<ISPort>",
    "Audience": "sneakerapi"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Please note that the Identity Server Project has Test Users Setup, the m2m user has a RedirectUrl set, this one is hardcoded for now so we need to modify it before testing authentication:

```csharp
new Client
{
    ClientId = "m2m.client",
    ClientName = "Client Credentials Client",

    AllowedGrantTypes = GrantTypes.ClientCredentials,
    ClientSecrets = { new Secret("82124972-4616-4d29-9c16-ccd29bc5d157".Sha256()) },

    AllowedScopes = { "sneakerapi.read", "sneakerapi.write" },
    RedirectUris = { "https://localhost:44347/swagger/oauth2-redirect.html" },
    AllowedCorsOrigins = new List<string>
    {
        "https://localhost:44347"
    }
}
```

## Features

- **Unit Testing with XUnit:**
  - Embrace unit testing using the XUnit testing framework.

- **Identity Server Integration:**
  - Implement secure authentication and authorization using Identity Server and Configuring Swagger to Authenticate with it.

- **Domain-Driven Design (DDD):**
  - Following principles of Domain-Driven Design like Domain Entities, ValueObjects, Aggregate Roots.

- **Repository Pattern:**
  - Adopt the repository pattern for a structured and consistent approach to data access.

- **Clean Architecture:**
  - Design and organize the application using Clean Architecture principles.
  - Maintain separation of concerns with distinct layers like Domain, Application, and Infrastructure.

- **Configuration Settings:**
  - Utilize configuration settings to avoid hardcoding values.

- **Automapper for Object Mapping:**
  - Leverage AutoMapper to simplify object-to-object mapping.

- **Dependency Injection:**
  - Implement dependency injection for loose coupling and improved testability.

- **CORS Handling:**
  - Manage Cross-Origin Resource Sharing (CORS) for secure communication with client applications.

- **JWT Token Authentication:**
  - Use JSON Web Tokens (JWT) for secure and stateless authentication.

- **Application Services:**
  - Design application services to encapsulate business logic and orchestrate interactions between layers.

- **In-Memory Database for Testing:**
  - Employ an in-memory database for testing to simulate data interactions without affecting the actual database.

- **FluentAssertions for Assertions:**
  - Enhance unit tests with FluentAssertions for expressive and readable assertions.

- **AutoFaker for Test Data:**
  - Generate test data easily using AutoFaker for efficient and realistic test scenarios.

- **Configuration-Based Testing:**
  - Configure tests with dedicated settings for better test management.

- **MOQ:**
  - Implementing Mocks with Moq library.


  ## Needs Improvements

Currently, there are challenges with the authentication process in Swagger. However, a temporary workaround is available by manually obtaining an access token using the Identity Server project. To achieve this on Windows, run the following command:

```bash
curl -H "Content-Type: application/x-www-form-urlencoded" -d "client_id=m2m.client&scope=sneakerapi.read&client_secret=82124972-4616-4d29-9c16-ccd29bc5d157&grant_type=client_credentials" "https://localhost:<Port>/connect/token"
