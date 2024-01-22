## Microservice Template - User Profile with CQRS pattern

A basic user profile implementation with Command-and-Query Responsibility Segregation (CQRS) pattern and Azure Cosmos Db backend.

---
## Layers: 
1. API Layer (Relias.UserProfile.Cqrs.Api)
    * depends on Application Layer
2. Application Layer (Relias.UserProfile.Cqrs.App)
    * depends on Infrastructure Layer, Common Library
3. Infrastructure Layer (Relias.UserProfile.Cqrs.Infra)
    * depends on Common Library
4. Common Library (Relias.UserProfile.Common)

## Package List:

### API Layer:
- Azure.Identity 1.6.0
- Microsoft.ApplicationInsights.AspNetCore 2.21.0
- Microsoft.ApplicationInsights.Kubernetes 2.0.4
- Microsoft.Azure.AppConfiguration.AspNetCore 5.1.0
- Microsoft.FeatureManagement.AspNetCore 2.5.1
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets 1.16.1
- Serilog.AspNetCore 6.0.1
- Swashbuckle.AspNetCore 6.4.0

### Application Layer:
- AutoMapper 11.0.1
- FluentValidation.DependencyInjectionExtensions 11.1.0
- MediatR.Extensions.Microsoft.DependencyInjection 10.0.1

### Infrastructure Layer:
- IEvangelist.Azure.CosmosRepository 3.6.0
- Polly 7.2.3

### Common Library:
- FluentValidation 11.1.0
- Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer 5.0.0
- Microsoft.Extensions.Caching.Abstractions 6.0.0
- Microsoft.Extensions.Caching.Memory 6.0.1
- Microsoft.Extensions.Caching.StackExchangeRedis 6.0.7
- Microsoft.Extensions.Configuration.Abstractions 6.0.0
- Microsoft.Extensions.Configuration.Binder 6.0.0
- Microsoft.Extensions.DependencyInjection.Abstractions 6.0.0
- Microsoft.Extensions.Logging.Abstractions 6.0.1
- Serilog 2.11.0
- Swashbuckle.AspNetCore.SwaggerGen 6.4.0

## Setup:
WIP...