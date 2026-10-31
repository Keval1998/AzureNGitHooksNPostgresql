# PostgreSQL migrations

All database SQL is kept in the ordered `migrations/` directory. The .NET
application does not contain SQL statements; it calls PostgreSQL stored
procedures through Npgsql.

## Migration order

1. `001_create_reporting_schema.sql` — creates `departments`,
   `report_requests`, keys, defaults, and the composite index.
2. `002_seed_current_reporting_data.sql` — inserts the three departments
   and five report rows captured from the learner's database on 2026-10-31.
3. `003_add_report_processing_procedure.sql` — adds a unique constraint on
   `report_requests.report_name` and the `process_report_request` procedure.

Apply migrations in numeric order to a **fresh, empty database**:

```bash
docker compose up -d postgres
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/migrations/001_create_reporting_schema.sql
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/migrations/002_seed_current_reporting_data.sql
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/migrations/003_add_report_processing_procedure.sql
```

The existing learner database already has the schema and sample rows from
migrations 001 and 002. **Do not run migration 001 against that populated
database.** For the existing database, run only migration 003 to add the
procedure and its unique business-key constraint:

```bash
docker compose exec -T postgres psql -v ON_ERROR_STOP=1 -U studyuser -d studydb < database/migrations/003_add_report_processing_procedure.sql
```

If the seed snapshot needs to be restored, migration 002 is an upsert by id
and can be rerun before migration 003. Do not rerun migration 003 after it has
already succeeded: it adds a named unique constraint and is a one-time schema
migration. A migration runner/history table is not introduced in this learning
slice; apply each numbered migration once and record the applied version.

## Procedure contract

`process_report_request(p_report_id varchar(100))` inserts a report request
with status `processing`. A unique constraint on `report_name` makes repeat
queue deliveries harmless: an existing row is left unchanged. The current
queue model's `ReportId` is stored in `report_requests.report_name` because
the inspected table has no separate report_id column.

## .NET integration

`ReportProcessingService` receives the singleton `NpgsqlDataSource`, opens
and disposes a pooled connection per operation, and invokes the procedure
using a parameterized `CALL`. The queue function awaits the operation and
passes its cancellation token. Database exceptions are not swallowed, so the
Functions queue trigger can apply its configured failure/retry behavior.
