# Principles

IGAM v0.1 is based on seven core principles.

## Principle 1: Every Integration Has An Owner

An integration must have an explicitly identified owner.

The owner is accountable for the integration as a managed capability. This does not mean the owner must build every component or operate every platform directly. It means there is a named role, team, or organizational unit accountable for the integration's purpose, lifecycle, governance, and coordination.

Ownership should answer:

- who approves changes to the integration
- who coordinates with source and target system owners
- who ensures documentation is maintained
- who is accountable for lifecycle decisions

## Principle 2: Every Business Entity Has A Source Of Truth

Each business entity should have a defined authoritative system.

A business entity is something the organization cares about, such as customer, order, product, employee, invoice, asset, or supplier. When multiple systems store or modify the same entity, IGAM requires teams to identify which system is authoritative for each relevant attribute or entity state.

The source-of-truth decision prevents integrations from becoming hidden conflict-resolution mechanisms.

## Principle 3: Authority Must Be Explicit

The ability to change systems, schemas, contracts, and business rules must be identified.

Authority is different from ownership. A team may own an integration but not have authority to change a vendor API, a source system schema, a canonical data model, or a business rule. IGAM makes those boundaries visible so architecture decisions reflect actual control.

Authority should be explicit for:

- source system changes
- target system changes
- integration contract changes
- schema and message changes
- business rule changes
- vendor-controlled interfaces

## Principle 4: Architecture Follows Governance

Architecture choices should be driven by ownership, authority, criticality, operational constraints, and cross-cutting governance concerns such as privacy, security, compliance, data classification, retention, residency, and auditability.

For example, a convenience integration between two internally owned systems may tolerate a simple point-to-point approach. A mission-critical process involving several domains and vendors may require stronger contract governance, monitoring, versioning, and decoupling. An integration that carries personal, confidential, regulated, or retention-sensitive data may require additional architectural constraints even when the business process itself is not mission critical.

IGAM does not prescribe one topology as universally correct. It asks teams to choose architecture based on the governance situation.

## Principle 5: Evolution Is Inevitable

Integrations must be designed with change and versioning in mind.

Systems change, business rules change, vendors change APIs, data models evolve, and organizational ownership shifts. IGAM treats evolution as normal rather than exceptional.

Each integration should define:

- expected lifespan
- expected rate of change
- versioning approach
- compatibility expectations
- deprecation strategy
- review cadence

## Principle 6: Operational Responsibility Must Be Defined

Monitoring, support, incident handling, and maintenance responsibilities must be explicit.

An integration is not complete when data moves successfully once. It must be observable, supportable, and maintainable for as long as business processes depend on it.

Operational responsibility should cover:

- monitoring
- alerting
- incident response
- retry or recovery behavior
- business reconciliation
- support ownership
- maintenance windows
- service-level expectations

## Principle 7: AI Compatibility

Governance and architecture artifacts should be expressible in structured formats that are understandable by humans, AI systems, and automation tools.

AI should be supported but not required.

IGAM artifacts should be written clearly enough for humans and structured enough for automated analysis. YAML, JSON, and similar formats can support AI-assisted assessment, documentation generation, architecture review, and governance validation, including validation of privacy, classification, retention, compliance, and auditability metadata.

AI compatibility does not mean delegating governance decisions to AI. It means making the artifacts explicit enough that AI and automation can assist responsibly.
