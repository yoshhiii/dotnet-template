using Azure.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using Relias.{{cookiecutter.solution_name}}.Api.Filters;
using Relias.{{cookiecutter.solution_name}}.Common.Logging;
using Relias.{{cookiecutter.solution_name}}.Common.Versioning;
using Relias.{{cookiecutter.solution_name}}.Cqrs.App;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Azure App Configuration

string appConfigConnectionString = builder.Configuration["APPCONFIG_CONNECTION_STRING"];
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(appConfigConnectionString)
        // Configure KeyVault if you plan on referencing at least one KeyVault secret in your Azure App Config service
        .ConfigureKeyVault(kv =>
        {
            // Review https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-key-vault-references-dotnet-core?tabs=core5x#grant-your-app-access-to-key-vault
            // for granting your app access to KeyVault
            // In most cases, the below statement will provide most flexibility
            // kv.SetCredential(new DefaultAzureCredential());

            // To configure KeyVault access for this example:
            // 1. Create an app registration in Azure AD for Msvc-Template app
            // 2. Create a client secret in Msvc-Template app registration
            // 3. Create KeyVault access policy to grant Get and List Secret permissions to Msvc-Template app
            // 4. Capture the TenantId, ClientId and Client Secret values from the app registration
            // 5. Use the values above for credential configuration
            string tenantId = builder.Configuration["AZURE_TENANT_ID"] ?? throw new KeyNotFoundException("AZURE_TENANT_ID");
            string clientId = builder.Configuration["AZURE_CLIENT_ID"] ?? throw new KeyNotFoundException("AZURE_CLIENT_ID");
            string clientSecret = builder.Configuration["AZURE_CLIENT_SECRET"] ?? throw new KeyNotFoundException("AZURE_CLIENT_SECRET");

            kv.SetCredential(new ClientSecretCredential(tenantId, clientId, clientSecret));
        })
        // Read values with no label first (if any),
        // then override them with an environment-specific label (if any)
        .Select(KeyFilter.Any, LabelFilter.Null)
        .Select(KeyFilter.Any, builder.Environment.EnvironmentName);
});

// If you would like the environment variables to override azure app config values,
// (last in the chain wins), uncomment the line below.
//builder.Configuration.AddEnvironmentVariables();

builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement();

#endregion

# region Logging

var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.With(new CorrelationPropertyEnricher())
    .CreateLogger();

builder.Logging.AddSerilog(loggerConfig);

#endregion

#region Application Services

builder.Services.AddApplicationServices(builder.Configuration);

#endregion

#region Azure App Insights

// Connection string stored in Azure App Configuration
builder.Services.AddApplicationInsightsTelemetry(options => options.ConnectionString = builder.Configuration["MscvTemplate:ApplicationInsights:ConnectionString"]);
builder.Services.AddApplicationInsightsKubernetesEnricher();

#endregion

builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

#region Metadata

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(setupAction =>
{
    // This line avoids errors when no version is specified.
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    // This line sets the default Api Version to 1.
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    // This line helps us see which Api version is being used.
    setupAction.ReportApiVersions = true;
});

// This block defines the format of the versioning and uses our custom titles etc.
// from SwaggerGenOptionsWithVersionSupport.cs
builder.Services.AddVersionedApiExplorer(options =>
{
    options.SubstituteApiVersionInUrl = true;
    options.GroupNameFormat = "'v'VVV";
});

builder.Services.ConfigureOptions<SwaggerGenOptionsWithVersionSupport>();

#endregion

var app = builder.Build();

#region Metadata

if (!app.Environment.IsProduction())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

#endregion

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
