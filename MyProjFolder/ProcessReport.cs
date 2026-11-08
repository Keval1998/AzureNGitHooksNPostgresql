using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyProjFolder.Models;
using MyProjFolder.Services;

namespace MyProjFolder;

public class ProcessReport
{
    private readonly ILogger<ProcessReport> _logger;
    private readonly ReportProcessingService _reportProcessingService;

    private static readonly Meter ReportMeter = new("ReportService");

    private static readonly Counter<long> SuccessfulRegistrationCalls =
        ReportMeter.CreateCounter<long>(
            "report_registration_calls_succeeded_total",
            description: "Number of report registration calls completed successfully.");

    private static readonly Histogram<double> RegistrationDuration =
        ReportMeter.CreateHistogram<double>(
            "report_registration_duration_ms",
            unit: "ms",
            description: "Duration of report registration database calls.");

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
            new(JsonSerializerDefaults.Web);

        ReportRequest? request =
            JsonSerializer.Deserialize<ReportRequest>(message, jsonOptions);

        if (request is null || string.IsNullOrWhiteSpace(request.ReportId))
        {
            throw new InvalidOperationException(
                "Queue message must contain a non-empty reportId.");
        }

        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            await _reportProcessingService.RegisterReportRequestAsync(
                request.ReportId,
                cancellationToken);
        }
        finally
        {
            stopwatch.Stop();
            RegistrationDuration.Record(stopwatch.Elapsed.TotalMilliseconds);
        }

        SuccessfulRegistrationCalls.Add(1);

        _logger.LogInformation(
            "Registered report request for ReportId: {ReportId}",
            request.ReportId);
    }
}
