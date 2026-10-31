# PostgreSQL database scripts

Database files are separated by responsibility:

- `migrations/`: one-time schema changes only (CREATE/ALTER/INDEX/constraints).
- `seeds/`: one-time sample-data inserts for a fresh database.
- `stored-procedures/`: procedure definitions, separate from schema changes.
  These use `CREATE OR REPLACE PROCEDURE` and can be applied again safely.

## Existing local studydb (your current database)

Your database already contains the baseline tables and sample rows. Do not run
migration 001 or the seed script again.

Run these two files, in this order, from the repository root:

```bash
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/migrations/002_add_report_name_unique_constraint.sql
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/stored-procedures/process_report_request.sql
```

Run migration 002 exactly once. It adds a unique constraint and will fail if
the constraint already exists or if duplicate report names are present.
The stored-procedure file can be run repeatedly: `CREATE OR REPLACE` replaces
the procedure definition.

## Fresh empty database

Apply in this order:

```bash
docker compose up -d postgres
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/migrations/001_create_reporting_schema.sql
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/seeds/001_seed_current_reporting_data.sql
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/migrations/002_add_report_name_unique_constraint.sql
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/stored-procedures/process_report_request.sql
```

The seed file is deliberately a one-time insert script. It has no
`ON CONFLICT` clause and must not be rerun: doing so should fail on duplicate
primary keys rather than silently overwrite sample data. If it fails partway,
the surrounding transaction rolls back all seed inserts.

## What each SQL file does

| File | Purpose | Safe to rerun? |
|---|---|---|
| `migrations/001_create_reporting_schema.sql` | Baseline tables and index | No |
| `seeds/001_seed_current_reporting_data.sql` | Initial sample records | No |
| `migrations/002_add_report_name_unique_constraint.sql` | Add unique constraint | No |
| `stored-procedures/process_report_request.sql` | Create/replace procedure | Yes |

## Procedure contract

`process_report_request(p_report_id varchar(100))` inserts a report request
with status `processing`. The unique constraint on `report_name` makes
repeat queue deliveries harmless: an existing row is left unchanged. The
queue model's `ReportId` is stored in `report_requests.report_name` because
the inspected table has no separate `report_id` column.

## .NET integration

`ReportProcessingService` receives the singleton `NpgsqlDataSource`, opens
and disposes a pooled connection per operation, and invokes the procedure
using a parameterized `CALL`. The queue function awaits the operation and
passes its cancellation token. Database exceptions are not swallowed, so the
Functions queue trigger can apply its configured failure/retry behavior.


## Local Azurite startup and queue sender compatibility

Start Azurite in its own terminal from any directory:

```bash
azurite --skipApiVersionCheck --location /tmp/azurite --debug /tmp/azurite/debug.log
```

The `--skipApiVersionCheck` option is a local-emulator compatibility workaround for the observed Storage Queues SDK request API version `2026-10-06` being rejected by Azurite `3.37.0`. It skips the emulator's API-version check; it does not change production Azure Storage behavior or prove cloud compatibility. Keep the Azurite terminal running. If a later Azurite version supports the SDK API version natively, remove the workaround and verify the queue flow again.

To inspect a report row without fragile shell quoting, open an interactive psql session:

```bash
docker compose exec postgres psql -U studyuser -d studydb
```

Then run this SQL at the `studydb=#` prompt:

```sql
SELECT id, report_name, status, created_at
FROM report_requests
WHERE report_name = 'rpt-test-001';
```

Exit with `\\q`. Do not paste the SQL statement as several unquoted shell arguments to `psql -c`.
