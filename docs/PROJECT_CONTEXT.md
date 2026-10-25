# Project Context

Repository: Keval1998/AzureNGitHooksNPostgresql. Active branch: AzureFunctionsBegin. Do not touch main unless the learner explicitly asks.

## Purpose and learning style
Practical learning for Azure, PostgreSQL, and CI/CD, building on the learner's C#/.NET, Angular, SQL/MySQL, REST API, and some AWS experience. Goal: strengthen the profile for .NET backend/product-engineering roles and return to interview preparation on schedule. Teach in calm, small, hands-on chunks. Inspect files and environment before giving commands. Keep familiar backend concepts short; prioritize Azure-specific implementation and practical progress.

## Environment and constraints
- GitHub Codespaces is the current development environment.
- Azure CLI was checked previously and is not available; do not ask the learner to repeat the check.
- Learner cannot currently access Azure Portal and has decided not to sign up for an Azure trial or provide a card due to billing/account-management overhead.
- Do not create cloud resources or revisit account signup unless the learner explicitly asks. Continue with local Functions/Azurite, Azure-specific configuration/deployment learning, PostgreSQL, and CI/CD.
- Actual Azure deployment remains pending/unverified. Never present local emulation as cloud deployment.

## Project structure
- `MyProjFolder`: .NET 10 isolated-worker Azure Functions v4 project. Functions include `MyHttpTrigger`, `GenerateReport`, and `ProcessReport`. `host.json` sets queue `messageEncoding` to `none`.
- `MyProjFolder/Models/ReportRequest.cs`: worker-side request model.
- `QueueSender`: .NET 10 console app using `Azure.Storage.Queues`.
- `QueueSender/Models/ReportRequest.cs`: sender-side request model.
- `QueueSender/Program.cs` serializes a `ReportRequest` using `System.Text.Json` web defaults before sending to `report-requests`.
- `MyProjFolder/ProcessReport.cs` deserializes JSON with matching web defaults, validates nonblank `ReportId`, and logs the ID.
- No PostgreSQL implementation was found during the current repository inspection; inspect the current Codespaces environment and existing files before selecting the smallest practical PostgreSQL exercise.

## Current checkpoint
Local Azurite queue flow is working. Learner screenshot showed `ProcessReport` logging ReportId `rpt-test-001` and completing Succeeded in 221 ms. This verifies local happy-path runtime only. The assistant did not independently run a build; Azure deployment has not been tested. Local settings should remain untracked.

Azurite 3.37.0 previously rejected the Storage Queues SDK 12.28.1 API version; `--skipApiVersionCheck` was used as a local workaround. Verify actual versions/errors before reusing it.

## Decisions and roadmap
1. **Current focus: PostgreSQL practical learning.** Inspect whether PostgreSQL/psql is already available in Codespaces. Start with one small SQL exercise (table, insert, select, update/transaction as appropriate), then progress to PostgreSQL-specific syntax and procedures. Do not assume a database server is installed or introduce a paid hosted database.
2. **Next: CI/CD.** Build/test the existing .NET project using GitHub Actions; later learn deployment workflow concepts without claiming a cloud deployment occurred.
3. **Azure-specific learning continues locally** through Functions configuration, bindings, identity/RBAC concepts, and local testing. Real cloud deployment can be revisited only if access becomes straightforward and the learner opts in.
4. Keep sender and worker models separate for now; the queue boundary is JSON. Do not introduce a shared class library without a concrete need.
5. Queue retry/poison-message behavior is a later optional topic, not a reason to prolong the queue milestone.

Respect session boundaries, avoid unrelated cleanup, and distinguish repository-file verification, build verification, local runtime verification, and Azure deployment. See `docs/SESSION_LOG.md` for the latest handoff.