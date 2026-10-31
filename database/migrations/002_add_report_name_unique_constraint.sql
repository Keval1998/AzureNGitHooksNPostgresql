-- One-time schema change: enforce a unique report name so duplicate
-- queue deliveries can be ignored by the stored procedure.
-- Apply exactly once after the baseline schema and seed data exist.
BEGIN;

ALTER TABLE report_requests
    ADD CONSTRAINT uq_report_requests_report_name UNIQUE (report_name);

COMMIT;
