# Governance Artifacts

IGAM uses a small set of artifacts to make integration governance explicit.

The artifacts can be written as documents, tables, YAML, JSON, diagrams, or a combination of formats. The important requirement is that the information is clear, reviewable, and maintained.

## Ownership Matrix

The Ownership Matrix maps systems, entities, and integrations to owners.

It should answer:

- who owns each system
- who owns each business entity
- who owns each integration
- who is accountable for documentation and lifecycle decisions

Example:

| Item | Type | Owner | Notes |
| --- | --- | --- | --- |
| CRM | System | Sales Operations | Vendor-hosted SaaS. |
| Customer | Business Entity | Sales Operations | CRM is authoritative for customer profile. |
| Customer Sync | Integration | Integration Platform Team | Coordinates CRM-to-ERP synchronization. |

## Authority Matrix

The Authority Matrix maps systems, schemas, contracts, and business rules to change authorities.

It should answer:

- who can approve changes
- who can implement changes
- who must be consulted
- whether a vendor or external party controls part of the change

Example:

| Area | Authority | Approval Required | Notes |
| --- | --- | --- | --- |
| CRM API | CRM Vendor | Vendor release process | Organization has limited control over timing. |
| ERP Customer Schema | ERP Team | ERP Architecture Owner | Changes require regression testing. |
| Customer Sync Mapping | Integration Platform Team | Sales Operations and ERP Team | Mapping changes affect reconciliation. |

## Source-of-Truth Map

The Source-of-Truth Map defines authoritative systems for business entities.

It should answer:

- which system is authoritative for each entity
- whether authority differs by attribute
- whether there are derived, replicated, or cached copies
- how conflicts are resolved

Example:

| Business Entity | Source Of Truth | Consumers | Conflict Rule |
| --- | --- | --- | --- |
| Customer Profile | CRM | ERP, Data Platform | CRM wins for profile attributes. |
| Invoice | ERP | CRM, Data Platform | ERP wins for invoice state and amount. |

## Integration Contract

The Integration Contract defines the interface, schema, ownership, and evolution rules for an integration.

It should include:

- integration name and purpose
- source and target systems
- owner
- business entities involved
- interface type
- payload or schema reference
- versioning approach
- compatibility expectations
- error handling expectations
- operational responsibility
- deprecation rules

The contract does not need to duplicate every technical schema detail, but it should identify where those details are defined and who controls them.

## Data Classification Matrix

The Data Classification Matrix maps business entities or payload fields to governance classifications. It helps teams understand which integrations handle sensitive, regulated, personal, or otherwise constrained data before topology, logging, storage, replication, or retention decisions are made.

It should answer:

- which entities or attributes are handled by the integration
- how each entity or attribute is classified
- which owner or policy defines the classification
- whether special handling is required for access, logging, retention, residency, masking, or deletion

Template:

| Entity | Classification | Owner Or Policy | Architectural Notes |
| --- | --- | --- | --- |
| Product | Public | Product Operations | May be cached or published broadly. |
| Customer | Personal Data | Data Governance Policy | Limit replication; preserve deletion and consent handling. |
| Employee | Confidential | HR Data Policy | Restrict access and logging; review vendor transfers. |

A simple version may only include entity and classification:

| Entity | Classification |
| --- | --- |
| Product | Public |
| Customer | Personal Data |
| Employee | Confidential |

The matrix is not a legal classification exercise by itself. It is an integration governance artifact that records the classifications teams need in order to make architecture and operational decisions.

## Criticality Assessment

The Criticality Assessment defines business impact and operational requirements.

It should include:

- criticality level
- business impact of failure
- acceptable delay or downtime
- support expectations
- monitoring requirements
- incident response expectations
- recovery and reconciliation needs

Example:

| Field | Value |
| --- | --- |
| Criticality | Operational |
| Failure Impact | Orders may be delayed in fulfillment. |
| Maximum Acceptable Delay | 2 hours |
| Monitoring | Integration failures alert platform support. |
| Recovery | Failed messages can be retried after correction. |

## Recommended Artifact Set For v0.1

For each meaningful integration, IGAM recommends maintaining:

- one Ownership Matrix entry
- one Authority Matrix entry
- one Source-of-Truth Map entry for each relevant business entity
- one Integration Contract
- one Criticality Assessment
- one Data Classification Matrix for integrations that handle classified, personal, confidential, regulated, or retention-sensitive data

Small organizations can combine these into a single document. Larger organizations may maintain them in repositories, architecture tools, catalogs, or governance platforms.
