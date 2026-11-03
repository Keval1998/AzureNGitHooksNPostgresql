using MyProjFolder.Services;
using Npgsql;

namespace ReportProcessing.IntegrationTests;

public sealed class ReportProcessingServiceIntegrationTests
{
    [Fact]
    public async Task RegisterReportRequestAsync_PersistsProcessingRequest()
    {
        string connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__ReportingDb")
            ?? throw new InvalidOperationException(
                "Set ConnectionStrings__ReportingDb to the PostgreSQL test database connection string.");

        string reportId = $"integration-test-{Guid.NewGuid():N}";

        await using NpgsqlDataSource dataSource =
            NpgsqlDataSource.Create(connectionString);

        try
        {
            ReportProcessingService service = new(dataSource);

            await service.RegisterReportRequestAsync(
                reportId,
                CancellationToken.None);

            await using NpgsqlConnection verificationConnection =
                await dataSource.OpenConnectionAsync();

            await using NpgsqlCommand verificationCommand = new(
                """
                SELECT status
                FROM report_requests
                WHERE report_name = @report_id;
                """,
                verificationConnection);

            verificationCommand.Parameters.AddWithValue("report_id", reportId);

            object? result = await verificationCommand.ExecuteScalarAsync();

            Assert.Equal("processing", result);
        }
        finally
        {
            await using NpgsqlConnection cleanupConnection =
                await dataSource.OpenConnectionAsync();

            await using NpgsqlCommand cleanupCommand = new(
                "DELETE FROM report_requests WHERE report_name = @report_id;",
                cleanupConnection);

            cleanupCommand.Parameters.AddWithValue("report_id", reportId);

            await cleanupCommand.ExecuteNonQueryAsync();
        }
    }
}
