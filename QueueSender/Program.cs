using Azure.Storage.Queues;

var connectionString = "UseDevelopmentStorage=true";
var queueName = "report-requests";

var queueClient = new QueueClient(
    connectionString,
    queueName);

await queueClient.CreateIfNotExistsAsync();

var message = """{"reportId":"rpt-test-001"}""";

await queueClient.SendMessageAsync(message);

Console.WriteLine($"Message sent: {message}");
