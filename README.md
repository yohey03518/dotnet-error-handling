# dotnet-error-handling
Examples of error handling patterns in .NET applications

## Global Level
- Uses **Middleware** for application-wide error handling
- Centralizes unhandled exceptions logging
- Handle different types of exceptions with different actions

## Service Level
- Handles business logic exceptions
- **Decorators** 
- **Interceptors**
- **Result Pattern** 

### Controller Level
- Uses **Action Filters**/**Exception Filters**
- Validates requests and model state
- Other custom exception

### Minimal API
- Uses **EndpointFilters** for error handling
- Handles errors with **IExceptionHandler**
- Supports custom error handling per endpoint

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
