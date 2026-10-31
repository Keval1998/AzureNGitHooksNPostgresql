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
- Learner cannot currently access Azure Portal and explicitly decided not to create an Azure subscription/trial, enter card details, activate billing, or create cloud resources due to billing/account-management overhead. Do not revisit this unless the learner explicitly changes the decision.
- Do not create cloud resources or revisit signup unless explicitly asked. Continue local Functions/Azurite and Azure-specific configuration/deployment learning.
- Actual Azure deployment remains pending/unverified. Never present local emulation as cloud deployment.
- Compose credentials are disposable local-learning credentials only.

## Project structure
- `MyProjFolder`: .NET 10 isolated-worker Azure Functions v4 project. Functions include `MyHttpTrigger`, `GenerateReport`, and `ProcessReport`. `host.json` sets queue `messageEncoding` to `none`.
- `MyProjFolder/Models/ReportRequest.cs`: worker-side request model.
- `QueueSender`: .NET 10 console app using `Azure.Storage.Queues`.
- `QueueSender/Models/ReportRequest.cs`: sender-side request model.
- `QueueSender/Program.cs` serializes `ReportRequest` using `System.Text.Json` web defaults before sending to `report-requests`.
- `MyProjFolder/ProcessReport.cs` deserializes and validates queue JSON, then awaits `ReportProcessingService`.
- `MyProjFolder/Services/ReportProcessingService.cs` calls PostgreSQL procedure `process_report_request` through a parameterized Npgsql command. `NpgsqlDataSource` and the stateless service are registered as singletons.
- `docker-compose.yml`: local PostgreSQL 16 service, database `studydb`, user `studyuser`, named volume `postgres_data`, health check.
- `database/migrations/`: one-time, ordered schema DDL changes only. `database/seeds/`: one-time sample-data inserts. `database/stored-procedures/`: rerunnable `CREATE OR REPLACE PROCEDURE` definitions, separate from schema migrations.
- `docs/POSTGRESQL_ROADMAP.md`: practical learning roadmap and completion criteria.

## Verified and unverified checkpoints
- Local Azurite queue flow was verified by the learner's screenshot: `ProcessReport` logged `rpt-test-001` and completed Succeeded in 221 ms. This verifies local happy-path runtime only. Independent build and Azure deployment were not verified.
- PostgreSQL container health and successful psql connection were confirmed in the learner's 2026-10-25 screenshot. `SELECT current_database();` returned `studydb`. A first attempt used the misspelling `currrent_database()` and failed as expected.
- On 2026-10-31, learner screenshots confirmed the current `studydb` schema: `departments` and `report_requests` both have identity integer primary keys; `departments.name` is unique and non-null; `report_requests` has non-null `report_name varchar(100)`, nullable `status varchar(20)` defaulting to `pending`, non-null `created_at timestamptz` defaulting to `CURRENT_TIMESTAMP`, nullable `department_id` FK to `departments.id`, and B-tree index `(status, created_at DESC)`. Three departments and five report rows were visible. Schema migrations, one-time seeds, and rerunnable stored-procedure definitions are kept in separate `database/migrations/`, `database/seeds/`, and `database/stored-procedures/` folders.
- Learner screenshot on 2026-10-31 shows Azurite 3.37.0 rejecting the Storage Queues SDK request API version `2026-10-06` with HTTP 400. The emulator error itself recommends `--skipApiVersionCheck`; the documented startup command now includes it as a local-only workaround. This workaround is recommended but not yet verified by a successful queue send in this session.
- Local settings should remain untracked.

## Roadmap
The overall roadmap is maintained in `docs/STUDY_ROADMAP.md`. Azure is a continuing interview-learning track, not a cloud-provisioning task: local Functions/Azurite, configuration/bindings, identity/RBAC concepts, and deployment-pipeline understanding remain explicit outcomes. PostgreSQL and GitHub Actions CI/CD are hands-on priorities. Actual cloud deployment is excluded from the current target; do not create a trial/subscription, use a card, or provision cloud resources unless the learner explicitly changes this decision.

Current detailed PostgreSQL milestones are in `docs/POSTGRESQL_ROADMAP.md`. Keep sender and worker models separate for now; queue boundary is JSON. Queue retry/poison-message behavior is optional later, not a reason to prolong the queue milestone.

- Repository inspection on 2026-10-31 confirmed Npgsql 10.0.3 is referenced by `MyProjFolder`. The initial data-source registration compiled successfully according to the learner's screenshot. The stored-procedure integration is remote-file verified only; learner build/runtime validation is still pending. No `.github/workflows/` directory or dedicated test project was present in the earlier 2026-10-27 tree audit; CI/CD remains outstanding.

## Timeline
The latest recorded target is **2026-11-07** for the accelerated roadmap: PostgreSQL + CI/CD focus through Nov 1, then integration/interview-readiness work Nov 2–7. On each session, compare verified progress against this target; if the plan is slipping, state the trade-off and propose a scoped adjustment rather than silently changing the date.

Respect session boundaries and distinguish repository-file verification, build verification, local runtime verification, and Azure deployment. See `docs/SESSION_LOG.md` for the latest handoff.


## Interview-oriented coaching and weekly availability (2026-10-27)
The learner's goal is interview readiness for .NET backend/product roles. Practical work must map to interview explanations and include PostgreSQL/Npgsql integration, GitHub Actions CI/CD build/test with PostgreSQL validation, Azure Functions configuration/bindings/identity/logging/deployment concepts, and integrated review. Do not treat this as a generic learning course or omit CI/CD from schedule estimates.

Confirmed recurring availability: laptop Monday/Tuesday 60 minutes each; mobile Wednesday-Friday 30 minutes each; weekends 2 hours. For 2026-10-28 through 2026-11-07, planned capacity is 11 hours if all slots are used; this is not confirmed attendance or focused actuals. Provisional remaining work including contingency is 8.1-11.4 hours and requires repository/workflow validation. The schedule is borderline at the high end; Nov 7 remains the target.


## 2026-10-31 — Build and local emulator recovery state
- Learner screenshots confirmed both PostgreSQL setup scripts succeeded: the one-time unique constraint migration returned BEGIN/ALTER TABLE/COMMIT, and the procedure script returned CREATE PROCEDURE.
- The attempted psql -c SELECT was split into multiple command-line arguments, producing warnings; its one-row output is not accepted as proof of the intended filtered query. Use an interactive psql session for verification.
- A compile defect was exposed in Functions startup: `FunctionsApplication.CreateBuilder(args)` was assigned to `IHostApplicationBuilder`, but the Functions-specific extension methods require `FunctionsApplicationBuilder`. Corrected in this recovery commit.
- Azurite 3.37.0 rejected Storage Queues SDK request API version `2026-10-06` with HTTP 400. Documented Azurite's own suggested `--skipApiVersionCheck` workaround. Successful use is not yet evidenced.
- The prior review was inadequate because it did not require a build of changed code or inspect emulator/client API-version compatibility. Agent instructions now require exact-diff/API checks, changed-project build/tests where executable, emulator compatibility, command quoting/path checks, and explicit static-only status when build execution is unavailable.
- This session's connected GitHub tools can edit/fetch repository files but cannot run the Codespaces build, Azurite, or Functions. Build/runtime verification remains outstanding and is the learner's next step.
