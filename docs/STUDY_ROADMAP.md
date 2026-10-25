# Study Roadmap — Azure, PostgreSQL, CI/CD

## Goal
Build demonstrable skills for .NET backend/product-engineering roles by **2026-11-07**. The outcome is practical implementation and the ability to explain trade-offs—not simply completing tutorials.

## Constraints and working agreement
- Environment: GitHub Codespaces; keep sessions small and hands-on.
- Azure Portal access is unavailable, and Azure CLI was previously confirmed unavailable. The learner chose not to start an Azure trial or provide a card. Do not create cloud resources or revisit account setup unless explicitly asked.
- Therefore, learn Azure through the existing .NET isolated-worker Functions project, local Azurite, Functions configuration/bindings, identity/RBAC and deployment concepts. Actual Azure deployment remains pending; local emulation is not cloud deployment.
- PostgreSQL runs locally via Docker Compose in Codespaces.
- Study Coach owns sequencing, teaching, timeline, and review. Development Agent owns delegated code/config/docs changes. Review Agent criteria are used for independent review.
- After meaningful sessions, sync project context and dated session log; update milestone status only with evidence.

## Track A — Azure Functions (continues; not dropped)
**Already demonstrated locally:** QueueSender sends a JSON ReportRequest to Azurite; ProcessReport deserializes, validates ReportId, logs it, and completes successfully for rpt-test-001 (learner screenshot evidence, 2026-10-24).

Remaining outcomes:
1. Explain QueueSender → Azure Storage Queue/Azurite → queue-triggered Function.
2. Practise Function configuration and bindings; distinguish local settings from Azure app settings.
3. Understand host/runtime configuration, isolated worker, dependency injection, logging/telemetry, and execution verification.
4. Learn identity/RBAC, managed identity vs connection strings, without provisioning cloud resources.
5. Understand deployment pipeline and what evidence is required for real deployment. Actual cloud deployment is not completed/verified unless access is available and the learner opts in.

## Track B — PostgreSQL (current hands-on focus)
1. Verify Compose health and psql connection (confirmed by learner screenshot on 2026-10-25).
2. Create/inspect a reporting schema; constraints, primary/foreign keys, CRUD, joins.
3. PostgreSQL features: identity columns, RETURNING, ON CONFLICT, ILIKE, timestamps, basic JSONB.
4. Transactions and correctness.
5. Indexes and EXPLAIN / EXPLAIN ANALYZE.
6. PL/pgSQL function.
7. .NET integration using Npgsql, parameterized commands, async/cancellation and transactions.
8. Build a small reporting workflow with tested SQL and documented design decisions.

## Track C — CI/CD (after core PostgreSQL)
1. Inspect existing repository/project structure and current test/build capabilities.
2. Add or improve GitHub Actions to build/test the .NET solution.
3. Validate SQL/integration behaviour against a disposable PostgreSQL service in CI.
4. Explain triggers, jobs, steps, artifacts, secrets, environment separation, and deployment gates.
5. Do not claim Azure deployment just because a pipeline builds successfully.

## Timebox and checkpoints
- Through 2026-11-01: PostgreSQL practicals as the main hands-on focus, while reserving short review sessions for the existing Azure Functions implementation. Avoid re-teaching familiar queue fundamentals.
- 2026-11-02 to 2026-11-07: CI/CD integration, consolidate Azure/PostgreSQL evidence, interview explanations and final gap review.
- This is an aggressive target. Compare verified outcomes with the target each session. If behind, prioritise a coherent demonstrable vertical slice and explain trade-offs rather than silently changing the date.

## Completion evidence
- Azure: explain and locally demonstrate the queue-triggered Functions flow and configuration; state clearly if cloud deployment remains pending.
- PostgreSQL: demonstrate schema/constraints, CRUD/joins, transactions, query plans/indexes, a PL/pgSQL routine, and .NET/Npgsql integration.
- CI/CD: observed passing workflow for build/test and database validation, with limitations documented.
- Record what was implemented, tested, and remains pending.