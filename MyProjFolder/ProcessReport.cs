using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

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
        _logger.LogInformation(
            "Processing report request: {Message}",
            message);
    }
}
