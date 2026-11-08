using System.Text.Json;
using System.Diagnostics.Metrics;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyProjFolder.Models;
using MyProjFolder.Services;

namespace MyProjFolder;

public class ProcessReport
{
    private readonly ILogger<ProcessReport> _logger;
    private readonly ReportProcessingService _reportProcessingService;
    private readonly static Meter Meter = new ("ReportService");
    private static readonly Counter<long> ProcessedReports =
            Meter.CreateCounter<long>("reports_processed_total");

    public ProcessReport(
        ILogger<ProcessReport> logger,
        ReportProcessingService reportProcessingService)
    {
        _logger = logger;
        _reportProcessingService = reportProcessingService;
    }

    [Function("ProcessReport")]
    public async Task Run(
        [QueueTrigger(
            "report-requests",
            Connection = "AzureWebJobsStorage")]
        string message,
        CancellationToken cancellationToken)
    {
        JsonSerializerOptions jsonOptions =
            new JsonSerializerOptions(JsonSerializerDefaults.Web);

        ReportRequest? request =
            JsonSerializer.Deserialize<ReportRequest>(message, jsonOptions);

        if (request is null || string.IsNullOrWhiteSpace(request.ReportId))
        {
            throw new InvalidOperationException(
                "Queue message must contain a non-empty reportId.");
        }

        await _reportProcessingService.RegisterReportRequestAsync(
            request.ReportId,
            cancellationToken);
        ProcessedReports.Add(1);

        _logger.LogInformation(
            "Registered report request for ReportId: {ReportId}",
            request.ReportId);
    }
}
