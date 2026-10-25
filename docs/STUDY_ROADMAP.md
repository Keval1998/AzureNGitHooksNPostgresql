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
## Schedule variance and time tracking (reviewed 2026-10-25)

### Original estimate and plan
- The earlier integrated plan estimated **15h 45m–19h total focused work** for Azure fundamentals, GitHub Actions CI/CD, PostgreSQL revision, and an integrated .NET cloud-readiness lab.
- The accelerated schedule was later set to Oct 21–Nov 7: Azure queue milestone Oct 21–24; review Oct 25; PostgreSQL + CI/CD Oct 26–Nov 1; integration/interview readiness Nov 2–7.
- The Azure queue milestone took longer than planned due to queue/storage debugging. The local happy path was verified on Oct 24; invalid/poison handling was deliberately deferred.

### Honest status
- No reliable per-session focused-minute log was maintained, so actual total time spent and exact hours lost cannot be reconstructed from chat timestamps. Do not invent actuals.
- The prior assistant's estimate of 10–14 remaining available hours was a rough forecast, not a tracked fact, and it did not reconcile the original 15h45m–19h estimate or Azure overrun. Treat that forecast as withdrawn.
- Schedule variance is confirmed qualitatively: Azure debugging consumed more time than its original slot, and PostgreSQL setup/schema work is still underway as of Oct 25. Exact variance in hours is unknown.

### Recovery rule
- Starting with the next study session, record actual focused minutes at session end, plus milestone result and next step. When session duration is not provided or observable, mark it **not recorded** rather than estimating it.
- At each session start, show target milestone, planned minutes, cumulative planned hours, verified completion, remaining estimate, and buffer.
- Preserve a buffer by reserving the final 15–20% of remaining available time for integration, fixes, and review. If the original deadline cannot fit the remaining work plus buffer, recommend scope reduction or a revised deadline explicitly; do not silently compress the schedule.
- Do not count local Azure emulation as cloud deployment. Real Azure deployment is excluded from current estimates because the learner has not opted into Azure account/trial setup.
- Re-estimate the remaining plan by inspecting the actual repo/workflow and by sizing only the minimum demonstrable vertical slice; avoid counting optional topics as mandatory.
## Re-baselined remaining effort (2026-10-25; planning estimate, not actuals)
Estimate is for the minimum demonstrable vertical slice, excluding real Azure cloud deployment and optional deep dives:
- Azure configuration/bindings, identity/RBAC, deployment concepts: 2–2.5 focused hours.
- PostgreSQL schema/CRUD/joins/transactions/core features: 2.5–3.5 hours.
- PostgreSQL indexes/query plans, PL/pgSQL, and .NET/Npgsql integration: 3–4 hours.
- CI/CD build/test plus PostgreSQL validation in GitHub Actions: 2–3 hours.
- Integrated verification and interview explanations: 1–1.5 hours.
- **Base estimate: 10.5–14.5 focused hours.** Add 15–20% contingency (about 2–3 hours); total planning envelope **12.5–17.5 hours**.
- This is a scoped estimate, not a verified promise. Inspect existing workflow, tests, and actual integration state at the next technical checkpoint and re-estimate. If the work does not fit the remaining availability before Nov 7, propose a scope cut or date change explicitly.

### Daily/session time tracking
- Actual focused minutes for prior sessions are not reliably recorded and must remain unknown; do not infer them from message timestamps.
- Current session planned cap: 20 additional focused minutes to complete the PostgreSQL schema/identity decision and first insert only if the learner confirms they have that time. The exact remaining daily budget is **not measurable from current records**; the user's usual weekend availability was 2–2.5 hours combined, not a confirmed remaining balance for today.
- At the end of each session, record learner-reported or explicitly measured focused minutes. If neither is available, record `not recorded` and do not calculate a fictitious cumulative actual.
- Each checkpoint review must recalculate: completed evidence, remaining task estimates, total remaining base hours, buffer, available hours before target, schedule variance, and a go/adjust decision. Use a second review against the repository state before declaring estimates reliable.