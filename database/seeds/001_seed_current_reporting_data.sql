-- One-time sample data for a fresh database after migration 001.
-- Do not run more than once: these INSERT statements intentionally have no
-- ON CONFLICT clause. Explicit identity values require OVERRIDING SYSTEM VALUE.
BEGIN;

INSERT INTO departments (id, name)
OVERRIDING SYSTEM VALUE
VALUES
    (1, 'Sales'),
    (2, 'Inventory'),
    (3, 'Finance');

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
        TIMESTAMPTZ '2026-10-26 05:03:20.535095+00', 3);

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
