# Project Context

Repository: Keval1998/AzureNGitHooksNPostgresql. Active branch: AzureFunctionsBegin. Do not touch main unless the learner explicitly asks.

## Purpose and learning style
Practical learning for PostgreSQL, CI/CD, and Azure, building on the learner's C#/.NET, Angular, SQL/MySQL, REST API, and some AWS experience. Goal: develop demonstrable skills for .NET backend/product-engineering roles and return to interview preparation on schedule. Teach calmly in small hands-on chunks. Inspect files/configuration before giving commands. Keep familiar backend concepts brief and prioritize implementation.

## Environment and constraints
- GitHub Codespaces is the current environment.
- A PostgreSQL 16 Docker Compose service has been added in `docker-compose.yml`; this file is configuration, not proof the container has started successfully. Verify in the learner's Codespace.
- Azure CLI was checked previously and is unavailable; do not ask the learner to repeat the check.
- Learner cannot currently access Azure Portal and decided not to sign up for an Azure trial or provide a card due to billing/account-management overhead.
- Do not create cloud resources or revisit signup unless explicitly asked. Continue local Functions/Azurite and Azure-specific configuration/deployment learning.
- Actual Azure deployment remains pending/unverified. Never present local emulation as cloud deployment.

## Project structure
- `MyProjFolder`: .NET 10 isolated-worker Azure Functions v4 project. Functions include `MyHttpTrigger`, `GenerateReport`, and `ProcessReport`. `host.json` sets queue `messageEncoding` to `none`.
- `MyProjFolder/Models/ReportRequest.cs`: worker-side request model.
- `QueueSender`: .NET 10 console app using `Azure.Storage.Queues`.
- `QueueSender/Models/ReportRequest.cs`: sender-side request model.
- `QueueSender/Program.cs` serializes a `ReportRequest` using `System.Text.Json` web defaults before sending to `report-requests`.
- `MyProjFolder/ProcessReport.cs` deserializes JSON with matching web defaults, validates nonblank `ReportId`, and logs the ID.
- `docker-compose.yml`: local PostgreSQL 16 service, database `studydb`, user `studyuser`, named volume `postgres_data`, health check. Credentials are disposable local-learning credentials only.
- `docs/POSTGRESQL_ROADMAP.md`: practical roadmap and completion criteria.

## Current Azure checkpoint
Local Azurite queue flow was verified by the learner's screenshot: `ProcessReport` logged `rpt-test-001` and completed Succeeded in 221 ms. This verifies local happy-path runtime only. The assistant did not independently run a build; Azure deployment has not been tested. Local settings should remain untracked.
Azurite 3.37.0 previously rejected the Storage Queues SDK 12.28.1 API version; `--skipApiVersionCheck` was used as a local workaround. Verify actual versions/errors before reusing it.

## Roadmap
1. **Current focus: PostgreSQL practicals.** Start the Compose service in Codespaces; verify container health and run the first query. Then learn schema/constraints/CRUD/joins, PostgreSQL-specific features, transactions, indexes and EXPLAIN, PL/pgSQL functions, and .NET integration with Npgsql. Use one small milestone per session. Follow `docs/POSTGRESQL_ROADMAP.md`.
2. **Next: CI/CD.** Add or improve GitHub Actions to build/test the existing .NET project and validate SQL against a disposable PostgreSQL service. Learn deployment workflow concepts without claiming a cloud deployment occurred.
3. **Azure-specific learning continues locally** through Functions configuration, bindings, identity/RBAC concepts, and local testing. Real cloud deployment can be revisited only if access becomes straightforward and the learner opts in.
4. Keep sender and worker models separate for now; the queue boundary is JSON. Do not introduce a shared class library without a concrete need.
5. Queue retry/poison-message behavior is optional later, not a reason to prolong the queue milestone.

Respect session boundaries and distinguish repository-file verification, build verification, local runtime verification, and Azure deployment. See `docs/SESSION_LOG.md` for the latest handoff.
