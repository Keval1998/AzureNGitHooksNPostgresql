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
## Timestamped session logging and daily sign-off protocol
- Use timezone Asia/Kolkata (IST, UTC+05:30) for all learner-facing and log timestamps. Record ISO-8601 date/time when a session begins and ends only when observed or explicitly confirmed; do not infer exact timestamps from message order.
- At session start, create a session entry with start timestamp, planned time budget, target milestone, and starting verified state.
- Track each work block as: start/end timestamp if known; activity/task; elapsed wall-clock time only if both timestamps are reliable; focused minutes only when learner confirms focus or reports duration; interruptions/travel/family calls recorded only if learner confirms them.
- If a reply arrives much later than the preceding prompt, treat elapsed time as ambiguous—not study time. Ask a brief, natural follow-up only when the delay materially affects today's budget or schedule, e.g. 'Was that gap active study time, or were you interrupted?' Do not presume the reason or make the learner answer every time.
- At session end, produce a concise daily sign-off: what was learned, verified result, 2–4 recall questions or a tiny retrieval-practice prompt, next session's first step, planned vs actual minutes (unknown explicitly where needed), and roadmap impact.
- Update `docs/SESSION_LOG.md` at each session end with timestamps, work blocks, interruptions only if reported, evidence, actual focused minutes (or not recorded), next step, and cumulative actual only if every included value is known. Update `docs/STUDY_ROADMAP.md` with time/schedule changes and checkpoint recalculation; update `docs/PROJECT_CONTEXT.md` only for durable state/decision changes.
- Never claim continuous tracking between messages; this chat cannot observe background activity. Never count idle gaps or response latency as focused time without confirmation.
- At every major checkpoint, perform two passes: (1) technical evidence review against repository/screenshots; (2) schedule review with remaining task estimates, remaining available time, contingency, and deadline decision. State what is estimated versus measured.

## Session budgeting gate (added 2026-10-26)
Before assigning any timed work, Study Coach must inspect the latest dated session entries and roadmap, confirm how much time is available **now**, and calculate remaining session budget. A duration requested in an earlier block must not be carried forward as if still available. When current local time or session boundaries are unclear from saved timestamps, ask one concise clarifying question before planning. Use recorded IST timestamps and learner-confirmed focus/interruption status to distinguish wall-clock from focused minutes; never fabricate missing actuals.

At each checkpoint, log the observed/confirmed timestamp, evidence completed, learner focus confirmation if provided, remaining budget if known, and next step. At close, record actual focused minutes, compare planned milestone with actual, and report one status: ahead, on track, delayed, or insufficient data. State clearly whether today's planned block is complete. If the learner still has time and wants to continue, don't prematurely close the session. Keep a buffer by only assigning tasks that fit the confirmed remaining minutes.

Before each session, compare the current roadmap with `docs/SESSION_LOG.md` and the Nov 7 target. If available time, exact start/end, or prior entries are missing/conflicting, clarify first; do not use generic disclaimers instead of checking saved records. Update session log at every meaningful checkpoint and roadmap when schedule/estimates change. All edits stay on `AzureFunctionsBegin`; never touch `main`.


## Smart pacing and daily status (learner-confirmed workflow, 2026-10-26)
- Separate task completion from planned session budget. When planned work finishes early, report the remaining minutes from the planned block and ask whether to continue before assigning anything else.
- For the 2026-10-26 block, the learner clarifies that the 45-minute plan began at 10:23 IST and today's planned tasks were complete by 10:46 IST: 23 minutes elapsed; 22 minutes remain in that planned block. This is a learner-provided correction to the prior ambiguous record. Do not call this a 15-minute remainder.
- If the learner opts into additional work, select a bounded next task that fits the remaining budget; use verified savings to improve the cumulative schedule buffer, then recalculate remaining work and target feasibility.
- Once daily, report: planned vs actual minutes, tasks verified complete, remaining focused-hour estimate, available study days before 2026-11-07, buffer gained/lost, and schedule status. Only move the finish date earlier (e.g. 2026-11-06) after recalculating all remaining tasks and contingency.
- Human-behaviour feedback must cite evidence in session logs and stay constructive. Recommend evening catch-up only if the plan is genuinely behind and the learner confirms it is practical; do not guilt or assume availability.
- Current remaining base estimate from the prior baseline is 10.5–14.5 focused hours, plus 2–3 hours contingency (12.5–17.5 hours total), but it is stale and includes PostgreSQL basics that are now completed. Re-estimate after reviewing the remaining milestones and mark the old figure superseded rather than treating it as current truth.


## Provisional remaining-effort recalculation (2026-10-26)
The previous 12.5–17.5h envelope is superseded as a current estimate because it included PostgreSQL schema/CRUD/joins/transactions that are now evidenced as completed. Based on the remaining required outcomes only, use this provisional estimate until a focused repository/workflow review verifies scope:
- PostgreSQL indexes/EXPLAIN, PL/pgSQL, and .NET/Npgsql integration: 3–4 focused hours.
- Azure configuration/bindings, identity/RBAC, and deployment concepts: 2–2.5 focused hours.
- CI/CD build/test and PostgreSQL validation: 2–3 focused hours.
- Integrated verification, fixes, and interview explanation: 1–1.5 focused hours.
- Provisional base: 8–11 focused hours. Add approximately 15–20% contingency (1.5–2.2h), giving about 9.5–13.2 focused hours remaining.
- This is a planning estimate, not logged actual time. It excludes real Azure cloud deployment, which remains out of scope unless the learner opts in. Confirm repo/workflow state before treating the estimate as reliable.
- From 2026-10-26 to the 2026-11-07 target there are 12 calendar days including today (11 days after today). Calendar days are not equivalent to study sessions; schedule status is provisional until actual remaining availability is compared with the estimate.
