# PostgreSQL Learning Roadmap

## Outcome
Build practical, explainable PostgreSQL skills that can credibly support listing PostgreSQL on a .NET backend resume. This requires writing and debugging SQL against a real PostgreSQL server, understanding PostgreSQL-specific behavior, and integrating it with an application—not just reading concepts.

## Environment
Use the repository's Docker Compose service to run PostgreSQL 16 locally in GitHub Codespaces. It creates a development-only database named `studydb`, user `studyuser`, and a named Docker volume for persistence. Credentials in `docker-compose.yml` are local learning credentials only; never reuse them outside this disposable environment or for production.

## Milestones
1. **Environment and first query:** start the container, confirm health, connect with `psql` inside the container, run `SELECT version()` and `SELECT current_database()`.
2. **SQL fundamentals in PostgreSQL:** create a small reporting domain (reports and report items), constraints, primary/foreign keys, inserts, selects, updates, deletes, and joins.
3. **PostgreSQL-specific querying:** identity columns, `RETURNING`, `ON CONFLICT`, `ILIKE`, date/time types, JSONB basics, and careful comparison with MySQL syntax.
4. **Transactions and correctness:** commit/rollback, isolation overview, concurrent updates, constraints, and safe data changes.
5. **Indexes and query plans:** B-tree indexes, composite-index order, `EXPLAIN` / `EXPLAIN ANALYZE`, and evidence-based query tuning.
6. **Procedural SQL:** write and call a PL/pgSQL function; understand when a function is preferable to application-side logic. Cover procedures only after functions and transaction semantics are clear.
7. **.NET integration:** connect from a small .NET app using Npgsql, parameterized commands, async calls, cancellation, and transaction handling.
8. **Resume-ready mini-project:** implement a small reporting workflow with schema/migrations, tested queries, a function where justified, and documented performance/consistency decisions.
9. **CI/CD connection:** run build/tests and SQL validation in GitHub Actions; keep database credentials out of source and use service containers or disposable databases in CI.

## Completion bar
Do not claim PostgreSQL proficiency based only on installing the database. The learner should be able to explain and demonstrate schema design, joins, transactions, indexes/query plans, PostgreSQL-specific features, one PL/pgSQL routine, and .NET integration. Record what was actually implemented and tested. Keep each session to one milestone and preserve the existing Azure/CI/CD roadmap.
