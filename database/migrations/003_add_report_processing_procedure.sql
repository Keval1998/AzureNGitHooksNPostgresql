-- Add a stable business key and the queue-processing stored procedure.
-- The current sample data has distinct report_name values.
BEGIN;

ALTER TABLE report_requests
    ADD CONSTRAINT uq_report_requests_report_name UNIQUE (report_name);

CREATE OR REPLACE PROCEDURE process_report_request(
    IN p_report_id character varying(100)
)
LANGUAGE plpgsql
AS $procedure$
BEGIN
    INSERT INTO report_requests (report_name, status)
    VALUES (p_report_id, 'processing')
    ON CONFLICT (report_name) DO NOTHING;
END;
$procedure$;

COMMIT;
