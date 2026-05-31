# Integration Governance and Architecture Model (IGAM)

![IGAM icon](assets/IGAM-icon.png)

IGAM is a high-level model for governing integration landscapes before selecting implementation technologies, middleware, or integration patterns.

Many integration failures are not caused by message routing, protocol choice, or code quality alone. They are caused by unclear ownership, conflicting sources of truth, missing change authority, undefined operational responsibility, and unmanaged evolution. IGAM helps teams make those concerns explicit before architecture and implementation decisions harden.

IGAM complements implementation-oriented pattern catalogs such as Enterprise Integration Patterns (EIP). EIP helps describe how integrations can be implemented. IGAM helps determine what governance and architectural conditions should guide those implementation choices.

## Version

This repository contains IGAM v0.1, an initial documentation and schema release.

## What IGAM Covers

IGAM focuses on:

- ownership of systems, data, and integrations
- authority to change systems, schemas, contracts, and business rules
- sources of truth for business entities
- integration criticality and operational expectations
- topology and architectural decision guidance
- evolution, versioning, and deprecation
- cross-cutting governance concerns such as privacy, security, compliance, classification, retention, residency, and auditability as architectural constraints
- structured artifacts that can be read by people, AI systems, and automation tools

IGAM does not replace technical design, implementation patterns, API standards, event schemas, platform-specific architecture, legal advice, or compliance frameworks. It gives teams the governance context needed to choose those things responsibly.

## Repository Structure

```text
igam/
+-- README.md
+-- assets/
|   +-- IGAM-icon.png
+-- docs/
|   +-- 01-introduction.md
|   +-- 02-principles.md
|   +-- 03-dimensions.md
|   +-- 04-artifacts.md
|   +-- 05-ai-compatibility.md
|   +-- 06-roadmap.md
+-- schemas/
|   +-- igam-schema.yaml
+-- examples/
    +-- small-business.md
    +-- enterprise.md
    +-- vendor-integration.md
```

## Quick Start

1. Identify the integration scenario.
2. Describe the source and target systems.
3. Define the owner of the integration.
4. Map the source of truth for each business entity.
5. Identify who has authority to change systems, schemas, contracts, and rules.
6. Assess criticality and operational responsibility.
7. Identify cross-cutting governance concerns such as privacy, security, compliance, data classification, retention, residency, and auditability.
8. Select topology and implementation patterns using the governance context.
9. Define versioning, deprecation, and review expectations.

## IGAM Custom GPT

A custom GPT is available to help explore and apply IGAM:

- [Open the IGAM Custom GPT](https://chatgpt.com/g/g-6a1aefdbe5bc8191b50d2a745b101548-igam)

Use it as an assistant for drafting IGAM artifacts, reviewing integration scenarios, identifying missing governance information, and turning early integration notes into more structured ownership, authority, source-of-truth, criticality, topology, and evolution guidance.

The custom GPT is intended to support human review, not replace accountable governance decisions. Teams should validate its suggestions against their own systems, owners, risk profile, and operating model.

## Core Documents

- [Introduction](docs/01-introduction.md)
- [Principles](docs/02-principles.md)
- [Dimensions](docs/03-dimensions.md)
- [Governance Artifacts](docs/04-artifacts.md)
- [AI Compatibility](docs/05-ai-compatibility.md)
- [Roadmap](docs/06-roadmap.md)

## Status

IGAM v0.1 is intentionally lightweight. It is meant to establish vocabulary, artifacts, and decision structure. Future versions may add a formal assessment method, maturity model, expanded schema, AI-assisted review guidance, and case studies.
