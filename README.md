# Event Operator Proposal for C#

This repository contains a language feature proposal for extending C#'s event syntax with two new operators:

- `+==` — Idempotent event registration (adds handler only once)
- `*=` — Weak event registration (does not retain strong reference to handler)

## Motivation

The goal of these operators is to improve robustness, reduce runtime bugs caused by duplicate registrations or memory leaks, and bring event handling on par with other expressive C# features like `??`, `??=`, and `?.`.

## Files

- `event_operator_proposal.md` — Formal language proposal in Markdown format
- `demo_event_operator_project/Program.cs` — Sample implementation of the proposed semantics using regular C# code

## How to Run the Demo

To run the demo project, navigate to the `demo_event_operator_project` folder and compile using your C# compiler:

```bash
cd demo_event_operator_project
dotnet new console -o .
dotnet run
```

Expected output:

```
Handler added uniquely.
Handler already registered, ignoring.
Weak handler registered.
Handled!
Weak handled!
```

## A working example using legacy C#:
I’ve implemented the core idea of a weak event handler pattern in a production-ready form here:
```link
🔗 https://github.com/rudolfstepan/event-driven-framework/blob/master/src/EventDriven.Core/EventBus/WeakEventBase.cs on GitHub
```

This solution works, but it’s verbose and needs boilerplate logic that could be elegantly handled at the language level with a *= operator.

My proposal is therefore not just theoretical – it builds on real-world experience, and this code demonstrates both the need and the feasibility.

## Contributing

Feel free to open issues or submit pull requests. Suggestions and feedback are highly welcome!

## License

This project is licensed under the MIT License.
