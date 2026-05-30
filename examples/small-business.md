# Example: Small Business

This example shows a lightweight IGAM assessment for a small business connecting an online store, accounting system, and email marketing tool.

## Scenario

A small retailer sells products through an online store. Orders are sent to an accounting system for invoicing. Customer email addresses are also sent to an email marketing tool for newsletters.

## Ownership

| Item | Type | Owner |
| --- | --- | --- |
| Online Store | System | Business Owner |
| Accounting System | System | Finance Lead |
| Email Marketing Tool | System | Marketing Lead |
| Order Export | Integration | Business Owner |
| Newsletter Signup Sync | Integration | Marketing Lead |

## Source Of Truth

| Business Entity | Source Of Truth | Notes |
| --- | --- | --- |
| Product | Online Store | Product catalog is maintained in the store. |
| Order | Online Store | Orders originate in the store. |
| Invoice | Accounting System | Invoice number and payment state are authoritative in accounting. |
| Newsletter Subscriber | Email Marketing Tool | Subscription status is authoritative in the marketing tool. |

## Authority

| Area | Authority | Notes |
| --- | --- | --- |
| Store Order Data | Business Owner | Store plugins may change export behavior. |
| Accounting Import Format | Finance Lead | Changes must be tested before month-end. |
| Marketing Subscriber Fields | Marketing Lead | Consent fields must not be overwritten by store data. |

## Criticality

| Integration | Criticality | Reason |
| --- | --- | --- |
| Order Export | Operational | Failed order export delays invoicing and fulfillment. |
| Newsletter Signup Sync | Convenience | Failure affects marketing reach but not core order processing. |

## Topology

The order export is file-based because the accounting system supports scheduled CSV import.

The newsletter signup sync is API-based using the marketing tool's standard connector.

## Evolution

The expected rate of change is low. The main risks are plugin updates, accounting import changes, and consent rule changes.

Recommended evolution rules:

- review integrations quarterly
- test order export after store plugin updates
- preserve marketing consent fields during synchronization
- document manual recovery steps for failed order imports

## IGAM Summary

This scenario does not require heavy governance. It does require explicit ownership, source-of-truth decisions, and simple operational recovery steps. Without those, even a small integration landscape can become fragile.
