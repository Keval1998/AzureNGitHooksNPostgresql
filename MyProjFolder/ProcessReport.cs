using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyProjFolder.Models;

namespace MyProjFolder;

public class ProcessReport
{
    private readonly ILogger<ProcessReport> _logger;

    public ProcessReport(ILogger<ProcessReport> logger)
    {
        _logger = logger;
    }

    [Function("ProcessReport")]
    public void Run(
        [QueueTrigger(
            "report-requests",
            Connection = "AzureWebJobsStorage")]
        string message)
    {
        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var request = JsonSerializer.Deserialize<ReportRequest>(message, jsonOptions);

        if (request is null || string.IsNullOrWhiteSpace(request.ReportId))
        {
            throw new InvalidOperationException(
                "Queue message must contain a non-empty reportId.");
        }

        _logger.LogInformation(
            "Processing report request for ReportId: {ReportId}",
            request.ReportId);
    }
}
