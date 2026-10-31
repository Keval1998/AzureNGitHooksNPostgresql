-- Reproducible snapshot of the learner's current local reporting data
-- (captured from psql on 2026-10-31). Safe to re-run: rows are upserted by id.
-- Explicit identity values require OVERRIDING SYSTEM VALUE.
BEGIN;

INSERT INTO departments (id, name)
OVERRIDING SYSTEM VALUE
VALUES
    (1, 'Sales'),
    (2, 'Inventory'),
    (3, 'Finance')
ON CONFLICT (id) DO UPDATE
SET name = EXCLUDED.name;

INSERT INTO report_requests
    (id, report_name, status, created_at, department_id)
OVERRIDING SYSTEM VALUE
VALUES
    (1, 'Monthly Sales Report', 'processing',
        TIMESTAMPTZ '2026-10-25 07:45:15.838940+00', NULL),
    (2, 'Inventory Report', 'completed',
        TIMESTAMPTZ '2026-10-26 05:03:20.535095+00', 2),
    (3, 'Customer Report', 'completed',
        TIMESTAMPTZ '2026-10-26 05:03:20.535095+00', 1),
    (4, 'Revenue Report', 'processing',
        TIMESTAMPTZ '2026-10-26 05:03:20.535095+00', 3),
    (5, 'Audit Report', 'pending',
        TIMESTAMPTZ '2026-10-26 05:03:20.535095+00', 3)
ON CONFLICT (id) DO UPDATE
SET report_name = EXCLUDED.report_name,
    status = EXCLUDED.status,
    created_at = EXCLUDED.created_at,
    department_id = EXCLUDED.department_id;

-- Keep identity generators ahead of the explicitly inserted snapshot ids.
SELECT setval(
    pg_get_serial_sequence('departments', 'id'),
    COALESCE((SELECT MAX(id) FROM departments), 1),
    EXISTS (SELECT 1 FROM departments)
);

SELECT setval(
    pg_get_serial_sequence('report_requests', 'id'),
    COALESCE((SELECT MAX(id) FROM report_requests), 1),
    EXISTS (SELECT 1 FROM report_requests)
);

COMMIT;
