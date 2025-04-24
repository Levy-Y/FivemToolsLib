# Getting Started

Welcome! This guide will help you get up and running with **FivemToolsLib**.

> This project is still in early development. Expect breaking changes and evolving APIs.

---

## Prerequisites

Before you start, ensure your C# FiveM resource is set up correctly and your server uses the **QBCore framework**.

### Recommended Project Setup

We highly recommend using the official **CitizenFX C# template** to set up your resource:

- 🔗 [FiveM Docs – C# Runtime Setup](https://docs.fivem.net/docs/scripting-manual/runtimes/csharp/)
- The template uses **.NET Framework 4.5.2** by default, which is required for compatibility with the FiveM runtime.

---

## Installing the Library

Currently, you'll need to manually add the compiled `FivemToolsLib.dll` to your resource:

1. Clone the repo or download the latest release.
2. Compile the library or use a prebuilt DLL.
3. Add it to your C# resource project (`libs/` for example).
4. Reference it in your `.csproj`:

```xml
<ItemGroup>
    <Reference Include="FivemToolsLib" HintPath="libs/FivemToolsLib.dll"/>
</ItemGroup>
```

> I am planning on uploading the library to NuGet in the future for easier setup, stay tuned for that!