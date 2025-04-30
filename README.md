# Event Operator Proposal for C#

This repository contains a language feature proposal for extending C#'s event syntax with two new operators:

- `+==` — Idempotent event registration (adds handler only once)
- `*=` — Weak event registration (does not retain strong reference to handler)

## Motivation

The goal of these operators is to improve robustness, reduce runtime bugs caused by duplicate registrations or memory leaks, and bring event handling on par with other expressive C# features like `??`, `??=`, and `?.`.

## Files

- `event_operator_proposal.md` — Formal language proposal in Markdown format
- `demo_event_operator_project/Program.cs` — Sample implementation of the proposed semantics using regular C# code

## Status

Currently a draft proposal — feel free to comment or contribute.

## Author

Rudolf Stepan — Proposal originally developed as a conceptual improvement for safe and readable event handling in modern C#.

---

> "C# has evolved to protect us from `null` mistakes. Now let’s evolve it to protect us from event registration mistakes."
