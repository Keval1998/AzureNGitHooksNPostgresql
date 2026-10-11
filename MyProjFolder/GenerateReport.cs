using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjFolder;

public class GenerateReport
{
    private readonly ILogger<GenerateReport> _logger;

    public GenerateReport(ILogger<GenerateReport> logger)
    {
        _logger = logger;
    }

    [Function("GenerateReport")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult(
            new 
            {
                reportId= 123,
                status = "Queued"
            }
        );
    }
}
