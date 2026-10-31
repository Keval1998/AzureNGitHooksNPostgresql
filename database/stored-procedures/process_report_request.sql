-- Stored procedure definition is intentionally re-runnable.
-- CREATE OR REPLACE updates the procedure body without changing the table schema.
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
