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

## Cross-Cutting Governance Concerns

IGAM keeps ownership, authority, criticality, topology, and evolution as the core dimensions. Concerns such as privacy, compliance, data classification, auditability, data residency, and data retention are not additional dimensions. They cut across all dimensions and should be treated as architectural constraints that shape integration decisions. Security also cuts across every dimension, and IGAM gives it a dedicated governance chapter because trust boundaries, identity, authorization, data protection, secure operations, and incident response must be designed rather than appended.

These concerns help teams ask whether an integration is governable and operable under the obligations that apply to the data, systems, organizations, and jurisdictions involved. IGAM does not turn these concerns into legal advice or a compliance framework. It uses them to make governance and architecture implications visible before technology choices are finalized.

### Privacy

Privacy concerns arise when integrations handle personal data or special category data. Teams should consider whether the integration collects, transforms, replicates, stores, or exposes more data than necessary; how data minimization is enforced; how right-to-erasure or deletion requests propagate; and how the full data lifecycle is governed from creation through retention, archival, and disposal.

Privacy can influence IGAM dimensions in several ways:

- Ownership: data owners and integration owners must be clear about who is accountable for personal data moving through the integration.
- Authority: schema, mapping, retention, and deletion behavior may require specific approval before change.
- Topology: unnecessary replication, broad event publication, or uncontrolled caches may be inappropriate for sensitive personal data.
- Evolution: versioning, deprecation, migration, and retirement plans must account for data lifecycle obligations and deletion propagation.

### Security

Security concerns include confidentiality, integrity, access control, identity, secrets, platform boundaries, and trust boundaries. Integrations often connect systems with different identities, networks, privilege models, vendors, operational controls, and support paths. The architecture should make explicit which systems and parties are trusted, where trust boundaries are crossed, how access is granted, how sensitive data is protected, how integrity is preserved, and how secure operations such as replay, retry, dead-letter handling, credential rotation, and incident response are governed.

Security requirements may influence interface style, authentication and authorization, encryption, network placement, message validation, logging, monitoring, error handling, segregation of duties, and topology. Higher security requirements may discourage direct point-to-point access, require controlled gateways or private connectivity, restrict event subscribers, limit payload visibility in diagnostics, or require dedicated queues, topics, broker namespaces, tenants, or environments. See [Integration Security Governance](06-integration-security-governance.md) for the dedicated IGAM security chapter.

### Compliance

Compliance concerns include regulatory requirements, industry standards, and legal obligations that affect integration behavior. Examples may include GDPR, PCI-DSS, HIPAA, SOX, or similar frameworks, depending on organizational context. IGAM does not define compliance controls for these frameworks. Instead, it records that compliance obligations exist and asks how they constrain ownership, authority, topology, operations, and evolution.

Compliance constraints may require named approval authorities, stronger change control, evidence of testing, specific retention rules, restricted data movement, audit reporting, or documented operational procedures.

### Data Classification

Data classification identifies the sensitivity and governance expectations of data handled by integrations. Example classifications include Public, Internal, Confidential, Personal Data, and Special Category Data. Organizations may adapt these labels to match their own policies.

Classification affects architecture and governance decisions by influencing allowed consumers, integration topology, access controls, logging behavior, storage locations, masking or tokenization needs, retention periods, and review rigor. A low-criticality integration may still require strong controls if it carries highly classified data.

### Auditability

Auditability concerns include traceability, change history, operational accountability, and regulatory reporting. Teams should be able to understand what data moved, which version of a contract or mapping was used, who approved relevant changes, which failures occurred, and how incidents were resolved.

Auditability may influence logging, correlation identifiers, immutable event or file records, approval workflows, change history, reconciliation reports, and operational dashboards. The goal is to support accountability and evidence without turning integrations into unmanaged repositories of sensitive data.

## Dimension Summary

An IGAM assessment should produce a concise profile similar to this:

| Dimension | Example |
| --- | --- |
| Ownership | Sales Operations owns the integration; CRM and ERP have separate system owners. |
| Authority | ERP team controls order schema; vendor controls CRM API; integration owner controls mappings. |
| Criticality | Operational. Sales order delays affect fulfillment but do not immediately halt the business. |
| Topology | Event-driven from CRM to integration platform, then API-based submission to ERP. |
| Evolution | Versioned contract, quarterly review, six-month deprecation window. |
