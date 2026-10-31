using Npgsql;

namespace MyProjFolder.Services;

public sealed class ReportProcessingService
{
    private readonly NpgsqlDataSource _dataSource;

    public ReportProcessingService(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task RegisterReportRequestAsync(
        string reportId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(reportId))
        {
            throw new ArgumentException(
                "Report ID cannot be empty.",
                nameof(reportId));
        }

        if (reportId.Length > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(reportId),
                "Report ID cannot exceed 100 characters.");
        }

        await using (NpgsqlConnection connection =
            await _dataSource.OpenConnectionAsync(cancellationToken))
        await using (NpgsqlCommand command =
            new NpgsqlCommand(
                "CALL process_report_request(@report_id);",
                connection))
        {
            command.Parameters.AddWithValue("report_id", reportId);

            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}
