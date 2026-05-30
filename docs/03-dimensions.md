# Dimensions

IGAM analyzes an integration through five primary dimensions:

- ownership
- authority
- criticality
- topology
- evolution

These dimensions help teams describe the governance and architectural shape of an integration before implementation patterns are selected.

## Ownership

Ownership identifies who is accountable for the systems, data, and integration.

Key questions:

- Who owns the source system?
- Who owns the target system?
- Who owns the data?
- Who owns the integration?
- Is ownership assigned to a named team, role, vendor, or business unit?
- Is ownership stable, shared, or unclear?

Ownership should be documented separately for systems, entities, and the integration itself. A common failure mode is assuming that system ownership automatically implies integration ownership.

## Authority

Authority identifies who can approve or perform changes.

Key questions:

- Who can modify source systems?
- Who can modify target systems?
- Who controls schemas?
- Who controls contracts?
- Who controls business rules?
- Are vendors involved?
- Are there regulatory, security, or compliance constraints?

Authority should be mapped even when the answer is uncomfortable. If a vendor controls an interface and the organization has limited influence over change timing, that fact should affect architecture, testing, versioning, and operational planning.

## Criticality

Criticality describes the business impact of integration failure.

IGAM v0.1 defines three levels.

| Level | Description | Typical Expectations |
| --- | --- | --- |
| Convenience | Helpful but not essential. Failure causes inconvenience or manual effort. | Basic monitoring, lightweight support, simple recovery. |
| Operational | Required for normal business operations. Failure disrupts teams, customers, or processes. | Defined support ownership, alerting, recovery procedures, change control. |
| Mission Critical | Essential to core business, safety, revenue, compliance, or customer commitments. | Strong governance, high observability, incident response, tested recovery, versioning, formal change authority. |

Criticality should drive architectural and operational decisions. Higher criticality generally requires stronger contracts, clearer ownership, better monitoring, and more deliberate evolution.

## Topology

Topology describes the structural style of the integration.

Common examples:

- point-to-point
- hub-and-spoke
- event-driven
- file-based
- human-in-the-loop

Topology should not be chosen only because a platform supports it. It should reflect ownership, authority, criticality, operational needs, and expected evolution.

For example:

- Point-to-point may be acceptable for simple, low-criticality integrations with stable ownership.
- Hub-and-spoke may help centralize control when many systems share common integration needs.
- Event-driven architecture may support decoupling and scalability, but requires event ownership, schema governance, and operational maturity.
- File-based exchange may be practical for vendors or legacy systems, but requires clear reconciliation and failure handling.
- Human-in-the-loop flows may be appropriate where judgment, approval, or exception handling is part of the process.

## Evolution

Evolution describes how the integration is expected to change over time.

Key questions:

- What is the expected lifespan?
- What is the expected rate of change?
- How are versions managed?
- How are consumers notified of changes?
- How are old versions deprecated?
- What compatibility guarantees exist?
- How often should the integration be reviewed?

Evolution planning is especially important when multiple consumers, vendors, or business-critical processes depend on the integration.

## Dimension Summary

An IGAM assessment should produce a concise profile similar to this:

| Dimension | Example |
| --- | --- |
| Ownership | Sales Operations owns the integration; CRM and ERP have separate system owners. |
| Authority | ERP team controls order schema; vendor controls CRM API; integration owner controls mappings. |
| Criticality | Operational. Sales order delays affect fulfillment but do not immediately halt the business. |
| Topology | Event-driven from CRM to integration platform, then API-based submission to ERP. |
| Evolution | Versioned contract, quarterly review, six-month deprecation window. |
