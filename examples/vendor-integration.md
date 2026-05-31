# Example: Vendor Integration

This example shows how IGAM can be used when an external vendor controls part of the integration landscape.

## Scenario

An organization integrates its internal HR system with a vendor-managed benefits platform. Employee eligibility, employment status, and selected benefits must stay synchronized.

## Ownership

| Item | Type | Owner |
| --- | --- | --- |
| HR System | System | People Technology |
| Benefits Platform | System | Benefits Vendor |
| Employee Eligibility Feed | Integration | People Technology |
| Benefits Enrollment Callback | Integration | People Technology with Benefits Vendor |

## Source Of Truth

| Business Entity | Source Of Truth | Notes |
| --- | --- | --- |
| Employee | HR System | HR owns employee identity, role, and employment status. |
| Benefits Eligibility | HR System | Eligibility rules are defined internally. |
| Benefit Selection | Benefits Platform | Vendor platform owns employee benefit selections after enrollment. |

## Authority

| Area | Authority | Notes |
| --- | --- | --- |
| HR Employee Data | People Technology | Internal HR governance applies. |
| Eligibility Rules | Benefits Team | Rule changes must be approved before open enrollment. |
| Vendor API | Benefits Vendor | Vendor controls API behavior and release timing. |
| Integration Mapping | People Technology | Mapping changes require vendor coordination. |
| Benefit Selection Schema | Benefits Vendor | Changes must be tested in vendor sandbox. |

## Criticality

| Integration | Criticality | Reason |
| --- | --- | --- |
| Employee Eligibility Feed | Mission Critical | Incorrect eligibility can affect employee benefits and compliance. |
| Benefits Enrollment Callback | Operational | Delays affect HR records and employee support. |

## Topology

The employee eligibility feed is file-based because the vendor requires scheduled secure file transfer.

The enrollment callback is API-based because the vendor exposes enrollment updates through a webhook and retrieval API.

## Cross-Cutting Governance Concerns

| Concern | Notes |
| --- | --- |
| Privacy | Employee data may include personal data and benefits-related attributes. The integration should minimize fields sent to the vendor and define how lifecycle events such as termination or deletion requests are handled. |
| Security | The file feed and callback cross an internal-to-vendor trust boundary. Access control, secure transfer, integrity checks, and vendor operational responsibilities must be documented. |
| Compliance | Employment, benefits, privacy, or financial reporting obligations may constrain timing, retention, evidence, and change approvals. IGAM captures these as architecture constraints, not as legal advice. |
| Data Residency And Retention | Vendor storage locations and retention behavior should be known before sensitive employee data is transferred. |
| Auditability | Eligibility file generation, vendor import results, enrollment callbacks, reconciliation reports, and mapping changes should be traceable for operational accountability. |

### Data Classification Matrix

| Entity | Classification |
| --- | --- |
| Employee | Confidential |
| Employee Contact Details | Personal Data |
| Benefits Eligibility | Confidential |
| Benefit Selection | Confidential |
| Health-Related Benefit Attribute | Special Category Data |

## Operational Responsibility

| Responsibility | Owner |
| --- | --- |
| Feed Generation Monitoring | People Technology |
| Vendor Import Monitoring | Benefits Vendor |
| Incident Coordination | People Technology |
| Employee Issue Triage | HR Support |
| Vendor Defect Resolution | Benefits Vendor |

## Evolution

Recommended evolution rules:

- define a calendar freeze period before and during open enrollment
- test vendor API and file format changes in sandbox
- require written notice for vendor breaking changes
- maintain reconciliation reports between HR and vendor records
- review operational runbooks before enrollment periods

## IGAM Summary

Vendor integrations require special attention to authority. The internal team may own the business outcome, but the vendor may control schemas, APIs, release timing, and operational visibility. IGAM makes those boundaries explicit so the architecture can account for them.
