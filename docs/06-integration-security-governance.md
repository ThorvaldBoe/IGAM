# Integration Security Governance

Integration security is a first-class part of IGAM. It is not a final checklist added after topology, message design, adapter selection, or platform configuration. Security governance defines how trust, access, confidentiality, integrity, observability, and operational accountability are designed into an integration from the beginning.

IGAM treats security as an architectural governance responsibility. Security controls are still implemented through concrete technologies such as identity providers, gateways, brokers, queues, certificates, secrets managers, network controls, encryption, logging, and monitoring platforms. The purpose of IGAM is to make the governance decisions behind those controls explicit, reviewable, and maintainable.

## Purpose

Integration Security Governance helps teams answer questions such as:

- Which systems, people, vendors, services, and automation identities are allowed to participate in the integration?
- Which trust boundaries does the integration cross?
- Which identities are used by producers, consumers, processors, adapters, operators, and support users?
- Which data requires confidentiality, integrity protection, masking, tokenization, or restricted logging?
- Who can approve access, credential changes, certificate rotation, topic or queue subscriptions, schema changes, and operational break-glass access?
- How are secrets, credentials, keys, certificates, and managed identities created, rotated, revoked, and audited?
- How are message replay, retry, dead-letter handling, and operational diagnostics protected from unauthorized data exposure or tampering?
- What evidence is required to demonstrate that the integration is operated securely?

Security governance should be documented before implementation choices become difficult to change.

## Security Is Governance, Architecture, And Operations

Security in IGAM spans three connected layers.

| Layer | Focus | Example Questions |
| --- | --- | --- |
| Governance | Accountability, authority, risk acceptance, and evidence. | Who approves consumers of an event? Who accepts residual risk? Who reviews privileged access? |
| Architecture | Trust boundaries, identity model, topology, data protection, and isolation. | Should the integration use a gateway, broker, private endpoint, separate tenant, or dedicated queue? |
| Operations | Monitoring, incident response, rotation, recovery, and auditability. | Who receives security alerts? How are compromised credentials revoked? How is replay controlled? |

An integration is not secure because it uses a secure transport protocol or a managed broker. It is secure when the architecture, operating model, and governance decisions match the sensitivity, criticality, trust boundaries, and threat profile of the integration.

## Security Governance Principles

### Security Ownership Must Be Explicit

Every integration should identify who owns security decisions for the integration. This may involve several roles:

- integration owner
- source system owner
- target system owner
- platform owner
- data owner
- security architect or security team
- vendor or managed service provider

The integration owner remains accountable for coordinating security governance even when specific controls are implemented by platform, security, or vendor teams.

### Trust Boundaries Must Be Visible

A trust boundary exists wherever data, control, identity, or operational responsibility crosses between parties, platforms, networks, domains, tenants, environments, or vendors.

Common trust boundaries include:

- internal system to external vendor
- production network to internet-facing endpoint
- operational technology to enterprise IT
- one business domain to another business domain
- application runtime to message broker
- integration platform to data platform
- synchronous API to asynchronous queue or topic
- managed cloud service to self-hosted system

IGAM artifacts should show where trust boundaries are crossed and which controls protect each crossing.

### Least Privilege Applies To People, Systems, And Messages

Integrations often fail least-privilege expectations because service accounts, shared credentials, broad subscriptions, or reusable adapters are granted more access than needed.

Least privilege should be applied to:

- producer and consumer permissions
- queue, topic, subscription, and event-stream access
- API scopes and roles
- database or file locations
- transformation and enrichment processors
- observability and support tooling
- replay, retry, and dead-letter operations
- deployment and configuration pipelines

Access should be specific enough to support the business purpose of the integration and no broader.

### Security Controls Must Follow Data Classification And Criticality

A convenience integration can still require strong security if it carries sensitive data. A mission-critical integration can still fail governance review if operational support users can view confidential payloads without control.

Security requirements should be shaped by:

- highest data classification handled by the integration
- privacy and compliance obligations
- business criticality
- number and type of consumers
- vendor involvement
- data residency and retention constraints
- ability to replay, reprocess, or reconstruct messages
- operational visibility needed for support

### Secure Operations Are Part Of The Integration Contract

Security should be included in integration contracts and operational runbooks. Teams should not depend on undocumented assumptions about credential rotation, access approval, monitoring, or incident response.

At minimum, secure operations should define:

- credential, key, certificate, and secret ownership
- rotation and revocation expectations
- access request and approval process
- monitoring and alert ownership
- security incident escalation path
- evidence and audit-log retention
- break-glass access controls
- dead-letter and replay authorization
- secure disposal of temporary files, cached payloads, and diagnostic exports

## Security Governance Domains

### Identity And Authentication

IGAM should identify the identities used by systems and operators. These may include service principals, managed identities, workload identities, certificates, API clients, user accounts, vendor accounts, and automation identities.

Key governance questions:

- Is each producer, consumer, processor, and adapter represented by a distinct identity?
- Are shared accounts avoided or formally risk-accepted?
- Who owns each identity?
- How are credentials issued, rotated, revoked, and monitored?
- Are identities separated by environment, tenant, domain, or criticality level?
- Is human access separated from workload access?

### Authorization And Access Control

Authorization determines what each participant may do. In message-driven integrations, this includes not only API permissions but also topic, queue, subscription, event-stream, and dead-letter access.

Key governance questions:

- Who can publish, consume, peek, purge, replay, or dead-letter messages?
- Who can create or modify subscriptions, routing rules, filters, schemas, or policies?
- Are access grants tied to business purpose and data classification?
- Are privileged operations separated from normal application processing?
- Are access reviews performed at a cadence that matches risk and criticality?

### Confidentiality And Data Protection

Confidentiality protects data from unauthorized disclosure across transport, processing, storage, logging, and diagnostics.

Key governance questions:

- Which fields or payloads require encryption, masking, tokenization, redaction, or suppression from logs?
- Are payloads stored in queues, topics, files, caches, dead-letter stores, or monitoring tools?
- How long can payloads remain in intermediate stores?
- Are diagnostic exports and support screenshots controlled?
- Are lower environments prevented from receiving production-sensitive data without approval and protection?

### Integrity, Validation, And Non-Repudiation

Integrity ensures messages are authentic, complete, valid, and not silently changed by unauthorized parties.

Key governance questions:

- How are messages validated against contracts or schemas?
- How are duplicate, stale, out-of-order, poisoned, or replayed messages handled?
- Are signatures, hashes, checksums, version identifiers, correlation identifiers, or immutable logs required?
- Who can modify transformation rules and enrichment logic?
- How are mapping changes reviewed and traced?

### Network, Platform, And Environment Boundaries

Integration topology should reflect security boundaries. A direct connection may be inappropriate when a controlled gateway, broker, private endpoint, network zone, tenant boundary, or dedicated environment is required.

Key governance questions:

- Which network zones, cloud accounts, tenants, subscriptions, or environments are involved?
- Is traffic public, private, hybrid, or vendor-managed?
- Are production and non-production environments isolated?
- Are broker namespaces, topics, queues, storage accounts, and integration runtimes shared or dedicated?
- Does the platform boundary align with ownership and operational responsibility?

### Message-Driven Security

Message-driven architectures introduce security responsibilities that differ from synchronous API integrations. Events, queues, topics, subscriptions, processors, retries, and dead-letter handling can create durable copies of data and operational paths that need explicit governance.

Key governance questions:

- Who owns the event or command contract?
- Who is authorized to subscribe to each event stream or topic?
- Are topic filters and routing rules treated as governed access decisions?
- Can messages be replayed, and who may authorize replay?
- How are poison messages and dead-letter queues protected?
- Are event payloads minimized for broad publication?
- Are correlation identifiers sufficient for traceability without exposing sensitive data?

For RabbitMQ, Azure Service Bus, or similar brokers, IGAM should document producer and consumer identities, broker namespace or vhost ownership, queue and topic permissions, dead-letter access, retention settings, encryption expectations, operational roles, and support procedures.

### Secrets, Keys, And Certificates

Secrets governance is part of integration lifecycle management.

Key governance questions:

- Where are secrets, keys, and certificates stored?
- Who can read, rotate, revoke, or recover them?
- Are secrets ever embedded in code, configuration files, build logs, payloads, or support tickets?
- What is the rotation cadence?
- What happens when a credential is suspected of compromise?
- Are certificate expiry and rotation monitored before outages occur?

### Logging, Monitoring, And Audit Evidence

Security logging must balance traceability with data minimization. More logs do not automatically mean better governance if logs expose payloads, credentials, or sensitive attributes.

Key governance questions:

- Which events must be logged for security accountability?
- Which fields must never be logged?
- Who can access logs, traces, dead-letter payloads, and operational dashboards?
- How are access changes, replay operations, failed authentication, authorization failures, schema violations, and suspicious message patterns detected?
- How long is security evidence retained?

### Incident Response And Recovery

Security incidents can affect integration availability, data correctness, confidentiality, and downstream consumers. Incident response should be part of the integration operating model.

Key governance questions:

- Who declares and coordinates an integration security incident?
- Which owners, vendors, consumers, and business stakeholders must be notified?
- How are credentials revoked or rotated during an incident?
- How are affected messages identified, quarantined, replayed, or reconciled?
- How are downstream consumers informed about potentially compromised or incorrect data?
- What post-incident evidence and remediation actions are required?

## Security Governance Profile

IGAM recommends maintaining a Security Governance Profile for integrations that cross trust boundaries, carry sensitive data, use shared integration platforms, support mission-critical processes, or involve vendors.

The profile should include:

- integration name and owner
- security decision owner or approver
- involved systems, platforms, vendors, and environments
- trust boundaries crossed
- data classifications and sensitive fields
- identities used by producers, consumers, processors, adapters, operators, and automation
- authentication and authorization model
- broker, queue, topic, subscription, API, file, or database permissions
- confidentiality and integrity controls
- secret, key, and certificate ownership and rotation
- logging, monitoring, and audit evidence expectations
- secure retry, replay, dead-letter, and diagnostic procedures
- incident response contacts and escalation paths
- residual risks and accepted exceptions
- review cadence

Example:

| Area | Example |
| --- | --- |
| Security Owner | Security Architecture with Integration Platform Team |
| Trust Boundaries | CRM SaaS to integration platform; integration platform to ERP; platform to data lake. |
| Identities | Distinct producer, consumer, processor, deployment, and support identities per environment. |
| Authorization | CRM may publish customer updates; ERP consumer may read only the customer synchronization subscription; support may inspect metadata but requires approval for payload access. |
| Data Protection | Personal data is encrypted in transit and at rest; sensitive attributes are redacted from logs and monitoring dashboards. |
| Replay And Dead Letter | Replay requires integration owner approval; dead-letter payload access is restricted and audited. |
| Secrets | Managed identities preferred; remaining secrets stored in approved secrets manager with monitored rotation. |
| Evidence | Access changes, failed authorization, replay actions, and schema validation failures are retained for audit review. |

## Relationship To Existing IGAM Artifacts

Integration Security Governance strengthens the existing IGAM artifacts rather than replacing them.

| IGAM Artifact | Security Governance Contribution |
| --- | --- |
| Ownership Matrix | Identifies who owns security decisions, identities, credentials, brokers, gateways, logs, and operational tooling. |
| Authority Matrix | Defines who can approve access, schemas, contracts, subscriptions, replay, credential rotation, and security exceptions. |
| Source-of-Truth Map | Clarifies which system is authoritative so security controls protect the correct data owner and prevent unauthorized mutation. |
| Integration Contract | Records authentication, authorization, data protection, validation, replay, error handling, and secure operational expectations. |
| Data Classification Matrix | Determines confidentiality, logging, storage, retention, masking, residency, and review requirements. |
| Criticality Assessment | Determines monitoring, incident response, recovery, access review, and evidence expectations. |
| Security Governance Profile | Connects trust boundaries, controls, owners, residual risks, and secure operations in one reviewable artifact. |

## Minimum Security Review Questions

Before an integration is approved, teams should be able to answer:

1. Who owns security governance for this integration?
2. What trust boundaries are crossed?
3. What is the highest data classification handled by the integration?
4. Which identities publish, consume, process, administer, deploy, and support the integration?
5. What can each identity do, and who approved that access?
6. Where can payloads be stored, logged, cached, dead-lettered, replayed, or exported?
7. How are confidentiality and integrity protected across transport, processing, storage, and diagnostics?
8. Who can change contracts, schemas, mappings, routing rules, subscriptions, and access policies?
9. How are secrets, keys, certificates, and credentials rotated and revoked?
10. How are security events monitored, escalated, investigated, and evidenced?
11. How are replay, retry, recovery, and reconciliation protected from unauthorized use?
12. What residual risks or exceptions have been accepted, by whom, and until when?

## Security Anti-Patterns

IGAM security governance should identify and challenge common anti-patterns:

- one shared service account used by many integrations
- broad publish or subscribe permissions without business justification
- support teams with unrestricted payload access by default
- dead-letter queues treated as low-risk technical storage
- sensitive payloads copied into logs, tickets, spreadsheets, or lower environments
- vendor integrations without documented trust boundaries or incident contacts
- schema and mapping changes without security review for sensitive attributes
- event streams with unknown or unmanaged consumers
- secrets stored in repositories, build variables, or unmanaged configuration files
- replay operations without approval, traceability, or consumer impact analysis
- production and non-production environments sharing credentials or message stores
- security exceptions without owner, expiry date, or remediation plan

## Practical Guidance By Integration Style

| Integration Style | Security Governance Emphasis |
| --- | --- |
| API-based | Client identity, scopes, gateway policy, rate limits, request validation, response filtering, and audit logs. |
| Event-driven | Publisher and subscriber authority, topic permissions, schema governance, payload minimization, replay control, and event retention. |
| Queue-based | Producer and consumer permissions, poison-message handling, dead-letter access, retry policy, and message visibility. |
| File-based | Transfer channel, file encryption, naming conventions, integrity checks, pickup and deletion rules, and vendor responsibilities. |
| Database integration | Credential scope, read/write segregation, change data capture authority, row or column sensitivity, and transaction impact. |
| Human-in-the-loop | User roles, approval evidence, segregation of duties, attachment handling, and manual exception auditability. |

## Review Cadence

Security governance should be reviewed when:

- a new integration is designed
- a new producer, consumer, vendor, topic, queue, file exchange, API client, or support role is added
- data classification changes
- credentials, certificates, broker namespaces, tenants, or network boundaries change
- replay or recovery procedures are modified
- an incident, audit finding, penetration test result, or control failure affects the integration
- the integration is promoted to a higher criticality level
- the integration contract or schema adds sensitive fields
- ownership or operational responsibility changes

High-criticality or sensitive integrations should also have a scheduled review cadence, even when no triggering change is known.
