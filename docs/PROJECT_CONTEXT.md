# Project Context

Repository: Keval1998/AzureNGitHooksNPostgresql. Active branch: AzureFunctionsBegin. Do not touch main unless the learner explicitly asks.

Purpose: practical learning for Azure, PostgreSQL, and CI/CD, building on the learner's .NET/C#, Angular, SQL/MySQL, REST API, and some AWS experience. Teach in calm, small, hands-on chunks. Inspect files and environment before giving commands. Avoid scope creep and respect the planned session boundary.

## Structure
- MyProjFolder: .NET 10 isolated-worker Azure Functions v4 project. Functions include MyHttpTrigger, GenerateReport, and ProcessReport. host.json sets queue messageEncoding to none.
- MyProjFolder/Models/ReportRequest.cs: worker-side request model.
- QueueSender: .NET 10 console app using Azure.Storage.Queues.
- QueueSender/Models/ReportRequest.cs: sender-side request model.
- QueueSender/Program.cs creates a ReportRequest and serializes it with System.Text.Json web defaults before sending to report-requests.
- MyProjFolder/ProcessReport.cs deserializes JSON with the same web defaults, validates nonblank ReportId, and logs the ID.

## Current checkpoint
Local Azurite queue flow is working. Learner screenshot showed ProcessReport logging ReportId rpt-test-001 and completing Succeeded in 221 ms. Relevant files were fetched from GitHub after changes. The assistant did not independently run a build; Azure deployment has not been tested. Local settings should remain untracked.

Azurite 3.37.0 previously rejected the Storage Queues SDK 12.28.1 API version; --skipApiVersionCheck was used as a local workaround. Verify actual versions/errors before reusing it.

## Decisions and next direction
Separate sender and worker models remain intentional for now; the queue boundary is JSON. Do not introduce a shared class library without a concrete need. The queue happy path is complete. Continue the wider roadmap toward PostgreSQL and CI/CD; retries/poison queues are a later optional lesson, not a reason to prolong the current session. See docs/SESSION_LOG.md for learner feedback and findings.