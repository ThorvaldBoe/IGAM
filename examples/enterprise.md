# Example: Enterprise

This example shows how IGAM can be applied in a larger organization with multiple domains, shared platforms, and mission-critical processes.

## Scenario

An enterprise integrates CRM, ERP, identity management, data platform, and customer support systems. Customer and order information moves across several domains. Multiple teams and vendors are involved.

## Ownership

| Item | Type | Owner |
| --- | --- | --- |
| CRM | System | Sales Technology |
| ERP | System | Finance and Operations Technology |
| Identity Management | System | Identity Platform Team |
| Data Platform | System | Data Engineering |
| Support Platform | System | Customer Operations |
| Customer Master Integration | Integration | Integration Platform Team |
| Order Status Integration | Integration | Integration Platform Team |

## Source Of Truth

| Business Entity | Source Of Truth | Notes |
| --- | --- | --- |
| Customer Profile | CRM | CRM owns customer profile and segmentation. |
| Customer Identity | Identity Management | Identity platform owns login identifiers and account status. |
| Order | ERP | ERP owns order state after booking. |
| Support Case | Support Platform | Support platform owns case state and resolution. |
| Analytical Customer View | Data Platform | Derived view only, not authoritative for operational updates. |

## Authority

| Area | Authority | Notes |
| --- | --- | --- |
| CRM API | Sales Technology and CRM Vendor | Vendor release schedule affects change timing. |
| ERP Order Schema | Finance and Operations Technology | Schema changes require architecture board review. |
| Customer Canonical Model | Data Governance Council | Changes require domain approval. |
| Integration Contracts | Integration Platform Team | Contract changes require affected consumer review. |
| Identity Attributes | Identity Platform Team | Security review required for sensitive attributes. |

## Criticality

| Integration | Criticality | Reason |
| --- | --- | --- |
| Customer Master Integration | Mission Critical | Customer identity, billing, and support processes depend on correct customer data. |
| Order Status Integration | Operational | Delays affect support and customer communications. |
| Support Case Analytics Feed | Convenience | Delays affect reporting but not case handling. |

## Topology

The enterprise uses a mixed topology:

- event-driven publication for customer and order state changes
- API-based lookup for current customer and order details
- batch ingestion into the data platform
- human-in-the-loop exception handling for failed identity matches

This topology reflects the different governance and operational needs of each flow.

## Evolution

Recommended evolution rules:

- all mission-critical contracts use explicit versioning
- event schemas are backward compatible within a major version
- consumers receive deprecation notices at least six months before breaking changes
- integration ownership is reviewed quarterly
- source-of-truth conflicts are escalated to the data governance council
- operational runbooks are required for mission-critical integrations

## IGAM Summary

The enterprise scenario requires formal governance because ownership and authority are distributed. The main architectural risk is not lack of technology. It is allowing integration behavior to evolve without clear authority, versioning, operational responsibility, or source-of-truth control.
