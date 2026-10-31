# PostgreSQL database scripts

These scripts document and reproduce the reporting schema used by the local
.NET / PostgreSQL study project.

## Layout

- `migrations/001_create_reporting_schema.sql`: creates `departments`,
  `report_requests`, their keys, defaults, and the composite index.
- `seeds/001_seed_current_reporting_data.sql`: inserts the three departments
  and five report requests visible in the learner's local database snapshot
  captured on 2026-10-31.

## Apply to a fresh database

Start the PostgreSQL Compose service, then run these commands from the
repository root in the Codespaces terminal:

```bash
docker compose up -d postgres
docker compose exec -T postgres psql -U studyuser -d studydb < database/migrations/001_create_reporting_schema.sql
docker compose exec -T postgres psql -U studyuser -d studydb < database/seeds/001_seed_current_reporting_data.sql
```

The migration is a one-time baseline for an **empty database**. It intentionally
does not use `IF NOT EXISTS`: silently accepting a pre-existing but different
schema could hide drift. Do not apply it to the learner's current database,
because that database already has these tables.

The seed script is safe to re-run for this snapshot: it upserts by primary-key
id and resets both identity sequences to the current maximum ids. It updates
the seeded rows to match this snapshot; it does not delete unrelated rows.

## Existing local database

The learner's existing `studydb` was inspected on 2026-10-31. Its schema and
records match the scripts above. To restore/update just the known sample rows
in that existing database, run only the seed script:

```bash
docker compose exec -T postgres psql -U studyuser -d studydb < database/seeds/001_seed_current_reporting_data.sql
```

## Current schema notes

- `departments.id`: integer identity primary key.
- `departments.name`: varchar(100), NOT NULL, UNIQUE.
- `report_requests.id`: integer identity primary key.
- `report_requests.report_name`: varchar(100), NOT NULL.
- `report_requests.status`: varchar(20), nullable, default `pending`.
- `report_requests.created_at`: timestamptz, NOT NULL, default
  `CURRENT_TIMESTAMP`.
- `report_requests.department_id`: nullable integer foreign key to
  `departments.id`.
- Index `idx_reports_status_created`: B-tree on
  `(status, created_at DESC)`.

The SQL files are the reproducible source of truth for this captured baseline.
Future schema changes should be added as new, ordered migration files; do not
rewrite an already-applied migration.
