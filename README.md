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

## Contributing

Feel free to open issues or submit pull requests. Suggestions and feedback are highly welcome!

## License

This project is licensed under the MIT License.
