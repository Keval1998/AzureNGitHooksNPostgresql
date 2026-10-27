# Project Context

Repository: Keval1998/AzureNGitHooksNPostgresql. Active branch: AzureFunctionsBegin. Do not touch main unless the learner explicitly asks.

## Roles and workflow
- Study Coach owns roadmap, learner-facing teaching, progress/time tracking, and independent review.
- `.github/agents/developer.agent.md` owns delegated implementation and documentation edits.
- `.github/agents/review.agent.md` is the independent review checklist/role.
- The Study Coach should delegate repository implementation instead of directly editing code/docs, then review the result and explain it to the learner. If the interface cannot invoke an autonomous named agent, be transparent that the written agent contract was followed using GitHub tools.
- Keep docs synchronized after meaningful progress: this file for durable context, `docs/SESSION_LOG.md` for dated results and next handoff, and `docs/POSTGRESQL_ROADMAP.md` for milestone status changes.

## Purpose and learning style
Practical learning for PostgreSQL, CI/CD, and Azure, building on C#/.NET, Angular, SQL/MySQL, REST API, and some AWS experience. Goal: demonstrable skills for .NET backend/product-engineering roles and return to interview preparation on schedule. Teach calmly in small hands-on chunks. Explain why before commands; inspect files/configuration before giving commands. Keep familiar backend concepts brief and prioritize implementation.

## Environment and constraints
- GitHub Codespaces is the current environment.
- Docker Compose PostgreSQL 16 is configured in `docker-compose.yml`. On 2026-10-25 the learner's screenshot confirmed container `study-postgres` was healthy, with host port 5432 published, and `psql` connected to `studydb`.
- Azure CLI was checked previously and is unavailable; do not ask the learner to repeat the check.
- Learner cannot currently access Azure Portal and decided not to sign up for an Azure trial or provide a card due to billing/account-management overhead.
- Do not create cloud resources or revisit signup unless explicitly asked. Continue local Functions/Azurite and Azure-specific configuration/deployment learning.
- Actual Azure deployment remains pending/unverified. Never present local emulation as cloud deployment.
- Compose credentials are disposable local-learning credentials only.

## Project structure
- `MyProjFolder`: .NET 10 isolated-worker Azure Functions v4 project. Functions include `MyHttpTrigger`, `GenerateReport`, and `ProcessReport`. `host.json` sets queue `messageEncoding` to `none`.
- `MyProjFolder/Models/ReportRequest.cs`: worker-side request model.
- `QueueSender`: .NET 10 console app using `Azure.Storage.Queues`.
- `QueueSender/Models/ReportRequest.cs`: sender-side request model.
- `QueueSender/Program.cs` serializes `ReportRequest` using `System.Text.Json` web defaults before sending to `report-requests`.
- `MyProjFolder/ProcessReport.cs` deserializes JSON with matching web defaults, validates nonblank `ReportId`, and logs the ID.
- `docker-compose.yml`: local PostgreSQL 16 service, database `studydb`, user `studyuser`, named volume `postgres_data`, health check.
- `docs/POSTGRESQL_ROADMAP.md`: practical learning roadmap and completion criteria.

## Verified and unverified checkpoints
- Local Azurite queue flow was verified by the learner's screenshot: `ProcessReport` logged `rpt-test-001` and completed Succeeded in 221 ms. This verifies local happy-path runtime only. Independent build and Azure deployment were not verified.
- PostgreSQL container health and successful psql connection were confirmed in the learner's 2026-10-25 screenshot. `SELECT current_database();` returned `studydb`. A first attempt used the misspelling `currrent_database()` and failed as expected.
- The learner created a table accidentally named `report_requets` (typo) with a manually assigned `INTEGER PRIMARY KEY`; then `\d report_requests` failed because that exact name doesn't exist. The SQL returned `CREATE TABLE`, so the misspelled table exists in the current local database. Recover cleanly by renaming it to `report_requests` (or drop/recreate if no data, explicitly explaining choice), and then verify the schema. Note that its `id` column is not yet auto-generated; do not silently claim it uses identity.
- Azurite 3.37.0 previously rejected Storage Queues SDK 12.28.1 API version; `--skipApiVersionCheck` was used as a local workaround. Verify actual versions/errors before reusing it.
- Local settings should remain untracked.

## Roadmap
The overall roadmap is maintained in `docs/STUDY_ROADMAP.md`. Azure is a continuing track, not dropped: local Functions/Azurite, configuration/bindings, identity/RBAC concepts, and deployment pipeline understanding remain explicit outcomes. PostgreSQL is the current hands-on focus; CI/CD follows, with Azure review and integration time reserved. Real Azure deployment remains pending unless access becomes available and the learner opts in.

Current detailed PostgreSQL milestones are in `docs/POSTGRESQL_ROADMAP.md`. Keep sender and worker models separate for now; queue boundary is JSON. Queue retry/poison-message behavior is optional later, not a reason to prolong the queue milestone.

## Timeline
The latest recorded target is **2026-11-07** for the accelerated roadmap: PostgreSQL + CI/CD focus through Nov 1, then integration/interview-readiness work Nov 2–7. On each session, compare verified progress against this target; if the plan is slipping, state the trade-off and propose a scoped adjustment rather than silently changing the date.

Respect session boundaries and distinguish repository-file verification, build verification, local runtime verification, and Azure deployment. See `docs/SESSION_LOG.md` for the latest handoff.


## Interview-oriented coaching and weekly availability (2026-10-27)
The learner's goal is interview readiness for .NET backend/product roles. Practical work must map to interview explanations and include PostgreSQL/Npgsql integration, GitHub Actions CI/CD build/test with PostgreSQL validation, Azure Functions configuration/bindings/identity/logging/deployment concepts, and integrated review. Do not treat this as a generic learning course or omit CI/CD from schedule estimates.

Confirmed recurring availability: laptop Monday/Tuesday 60 minutes each; mobile Wednesday-Friday 30 minutes each; weekends 2 hours. For 2026-10-28 through 2026-11-07, planned capacity is 11 hours if all slots are used; this is not confirmed attendance or focused actuals. Provisional remaining work including contingency is 8.1-11.4 hours and requires repository/workflow validation. The schedule is borderline at the high end; Nov 7 remains the target.
