# AI Compatibility

IGAM artifacts should be understandable by humans, AI systems, and automation tools.

AI compatibility is a design goal, not a framework dependency. Teams can use IGAM without AI. However, structured artifacts make it easier to support AI-assisted governance, review, documentation, and validation.

## Why Structure Matters

Integration governance often lives in slides, diagrams, chat threads, tickets, and undocumented assumptions. This makes it difficult for people and tools to reason consistently about ownership, authority, sources of truth, and operational responsibility.

Structured formats such as YAML or JSON help make governance information:

- explicit
- reviewable
- searchable
- comparable
- automatable
- easier for AI tools to analyze

## Example Capabilities

Structured IGAM artifacts can support:

- AI-assisted assessments
- automated governance validation
- documentation generation
- architecture review assistance
- consistency checks across integrations
- identification of missing owners or authorities
- source-of-truth conflict detection
- criticality and operational readiness review

## Human Authority Remains Required

AI can assist with analysis, but it should not be treated as the owner of governance decisions.

Human accountable parties should remain responsible for:

- approving ownership
- confirming sources of truth
- granting change authority
- accepting operational risk
- approving criticality levels
- making architecture decisions

AI outputs should be treated as recommendations, drafts, or review aids.

## Machine-Readable Artifacts

IGAM v0.1 includes a starter YAML schema in `schemas/igam-schema.yaml`.

The schema is intended to help teams represent:

- integrations
- systems
- business entities
- ownership
- authority
- source-of-truth mappings
- criticality
- topology
- evolution
- operational responsibility
- privacy, classification, retention, compliance, and auditability metadata

The schema is deliberately compact in v0.1. It should be extended as practical use cases emerge.

For example, this excerpt from an AI-compatible artifact can expose governance constraints without making the tool responsible for legal or compliance decisions:

```yaml
igamVersion: "0.1"
metadata:
  name: "Customer profile synchronization"
  owner: "Integration Platform Team"
  status: "reviewed"
integrations:
  - id: customer-profile-sync
    name: "Customer Profile Sync"
    criticality: operational
    topology: event-driven
    governanceConcerns:
      privacy:
        personalData: true
        specialCategoryData: false
        minimization: "Publish only attributes required by approved consumers."
        erasureHandling: "Deletion requests propagate through the integration contract."
      classification:
        highestClassification: "Personal Data"
        matrixReference: "docs/data-classification/customer-profile.md"
      compliance:
        obligations: ["GDPR-like privacy obligations"]
        notes: "Confirm applicable obligations with accountable governance and legal owners."
      retention:
        policy: "Do not retain payloads beyond operational retry window."
      auditability:
        traceability: "Correlation ID required for each published event."
        changeHistory: "Contract and mapping changes require review record."
```

This type of metadata helps AI systems and automation tools identify missing review points, inconsistent classifications, excessive retention, or weak auditability. It does not make IGAM dependent on AI and does not replace accountable human approval.

## Good AI-Compatible IGAM Artifacts

Good artifacts are:

- specific enough to be useful
- structured enough to validate
- readable enough for humans
- explicit about unknowns
- clear about ownership and authority
- versioned with the integration lifecycle

Avoid hiding uncertainty. If an owner, authority, or source of truth is unknown, mark it as unknown and create follow-up governance work.
