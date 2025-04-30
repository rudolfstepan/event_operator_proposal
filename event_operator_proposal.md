# Proposal: Enhanced Event Registration Syntax using `+==` and `*=` Operators

**Author:** [Your Name or Alias]  
**Status:** Draft  
**Version:** 1.0  
**Area:** Language Design – Events  
**Related Concepts:** Event Handling, Weak Delegates, Memory Safety, Idempotency

---

## Summary

Introduce two new operators for event registration in C#:

- `+==` for **idempotent event registration** – adds a handler only if it has not already been registered.
- `*=` for **weak event registration** – registers a delegate without creating a strong reference to the subscriber.

These operators aim to simplify common event-handling patterns, reduce memory leaks, and make intent clearer in event-driven programming.

---

## Motivation

C#'s current event registration mechanism using `+=` has long-standing limitations:

- It can lead to **accidental double registrations**, resulting in duplicated handler execution.
- It often causes **memory leaks**, especially when publishers strongly reference subscribers that are never explicitly deregistered.

While workarounds exist (such as `WeakEventManager` or manual de-duplication), they are verbose, error-prone, or non-standard.

This proposal introduces **syntactically concise, expressive, and semantically safe** alternatives.

---

## Syntax & Semantics

### `+==` – Idempotent Event Registration

```csharp
button.Click +== HandleClick;
```

Equivalent to:

```csharp
if (!IsHandlerAlreadyRegistered(button.Click, HandleClick))
    button.Click += HandleClick;
```

- Adds the event handler only if it's not already attached.
- Prevents unintentional duplicate registrations.
- Comparison based on reference equality of method group and target.

### `*=` – Weak Event Registration

```csharp
button.Click *= HandleClick;
```

Equivalent to:

```csharp
WeakEventHelper.Register(button, nameof(button.Click), HandleClick);
```

- Registers the delegate through a `WeakReference`, allowing the subscriber to be garbage collected.
- Useful in publisher-subscriber models, event buses, and long-lived observables.

---

## Compiler Behavior

- The compiler would translate both operators into helper logic, similar to existing patterns (e.g., `WeakEventManager`, deduplication).
- These operators are **syntactic sugar**, requiring no fundamental runtime changes.

### Compiler Warning Logic

To prevent accidental misuse or ambiguity:

- If the same handler is registered via both `+=` and `+==` on the same event, the compiler should emit a warning, e.g.:
  ```text
  Warning: Handler already registered using '+=' – additional '+==' is redundant or potentially unintended.
  ```

- This mimics existing C# warnings for potentially unsafe constructs, such as:
  - Assignment in condition (`if (x = y)`)
  - Unused `catch` blocks
  - Async methods lacking `await`

- The warning would help developers avoid unintended side effects due to misunderstanding of operator semantics.

---

## Benefits

- Prevents common runtime bugs due to over-registration and memory retention.
- Makes event-related code **more readable, declarative, and safe**.
- Supports cleaner event APIs in reusable libraries and large codebases.

---

## Alternatives Considered

- Extension methods (e.g., `.AddOnce(handler)` or `.AddWeak(handler)`) – require more code and reduce discoverability.
- External libraries (e.g., `WeakEventManager`) – verbose and not standardized across codebases.

---

## Drawbacks

- Introduction of new operators requires compiler and tooling updates (Roslyn, IDEs, analyzers).
- May slightly increase onboarding complexity for developers unfamiliar with the operators.
- Requires parser-level handling to avoid ambiguities or accidental typos.

---

## Syntax Consideration – `=+` Rejected

The syntax `=+` was considered but rejected due to:

- Potential ambiguity with valid C# usage (`x = +5`)
- No clear event-related semantic mapping
- High risk of silent misuse or confusion

By contrast, `+==` and `*=` remain consistent with C#'s existing event conventions (`+=`) while clearly expressing enhanced behavior.

---

## Conclusion

The proposed `+==` and `*=` operators offer a concise and expressive solution to long-standing issues in C# event handling. They provide real-world value, especially in large, long-lived, or UI-heavy applications, and align with modern principles of declarative, safe, and maintainable code.

---


---

## Similarities to Existing Operator Enhancements in C#

The C# language has a history of introducing small, focused syntactic features that significantly improve safety, readability, and intent expression—without altering the underlying runtime semantics. Notable examples include:

- `?.` – null-conditional operator
- `??` – null-coalescing operator
- `??=` – null-coalescing assignment
- `T?` – nullable value types

These operators all represent a shift from *manual defensive coding* to *concise, declarative expressions of intent*.

The proposed `+==` and `*=` operators follow the same philosophy:

> *C# has evolved to protect us from `null` mistakes. Now let’s evolve it to protect us from event registration mistakes.*

They are minimal in syntax, powerful in impact, and aligned with C#'s long-standing design philosophy of "make the common case easy and the risky case obvious."

---
