using System.Text.Json;
using Azure.Storage.Queues;
using QueueSender.Models;

var connectionString = "UseDevelopmentStorage=true";
var queueName = "report-requests";

var queueClient = new QueueClient(
    connectionString,
    queueName);

await queueClient.CreateIfNotExistsAsync();

var request = new ReportRequest
{
    ReportId = "rpt-test-001"
};

var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
var message = JsonSerializer.Serialize(request, jsonOptions);

await queueClient.SendMessageAsync(message);

Console.WriteLine($"Message sent: {message}");
