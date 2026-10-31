using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyProjFolder.Services;
using Npgsql;
using OpenTelemetry;

FunctionsApplicationBuilder builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")))
{
    builder.Services.AddOpenTelemetry()
        .UseFunctionsWorkerDefaults()
        .UseAzureMonitorExporter();
}

string reportingConnectionString =
    builder.Configuration.GetConnectionString("ReportingDb")
    ?? throw new InvalidOperationException(
        "Connection string 'ReportingDb' is not configured.");

builder.Services.AddSingleton<NpgsqlDataSource>(
    serviceProvider => NpgsqlDataSource.Create(reportingConnectionString));
builder.Services.AddSingleton<ReportProcessingService>();

builder.Build().Run();
