# dotnet-error-handling
Examples of error handling patterns in .NET applications

## Error Handling Layers

### Global Level
- Uses **Middleware** for application-wide error handling
- Provides consistent error responses
- Centralizes unhandled exceptions logging

### API Level
#### Controller-based API
- Uses **Action Filters**/**Exception Filters**
- Validates requests and model state

#### Minimal API
- Uses **EndpointFilters** for error handling
- Handles errors with **IExceptionHandler**
- Supports custom error handling per endpoint

### Service Level
- Uses **Interceptors** or **Decorators**
- Handles business logic exceptions
- Implements custom domain exceptions
- Validates business rules
- Uses **Result Pattern** for error handling

### External Communication
- Uses **HttpMessageHandler** to handle HTTP client errors
- Implements resilience patterns
  - Library: **Polly** for retries, circuit breaker, timeout
- Handles distributed system failures

### gRPC Communication
- Uses **Interceptors** for error handling
  - Server-side
  - Client-side
- Converts exceptions to gRPC **Status Codes**
