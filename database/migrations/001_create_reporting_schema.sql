-- Baseline schema for a fresh studydb database.
-- Apply once to an EMPTY database. Do not run against the learner's existing
-- database, which already contains these tables.
BEGIN;

CREATE TABLE departments (
    id integer GENERATED ALWAYS AS IDENTITY,
    name character varying(100) NOT NULL,
    CONSTRAINT departments_pkey PRIMARY KEY (id),
    CONSTRAINT departments_name_key UNIQUE (name)
);

CREATE TABLE report_requests (
    id integer GENERATED ALWAYS AS IDENTITY,
    report_name character varying(100) NOT NULL,
    status character varying(20) DEFAULT 'pending'::character varying,
    created_at timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    department_id integer,
    CONSTRAINT report_requests_pkey PRIMARY KEY (id),
    CONSTRAINT fk_report_department
        FOREIGN KEY (department_id) REFERENCES departments(id)
);

CREATE INDEX idx_reports_status_created
    ON report_requests USING btree (status, created_at DESC);

COMMIT;
