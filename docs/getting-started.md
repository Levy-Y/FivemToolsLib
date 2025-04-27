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

The recommended way to install FivemToolsLib is via NuGet:

1. Open your C# resource folder.

2. Run the following command in your project directory to install the package:
    ```sh
    dotnet add package FivemToolsLib.Client
    ```
   or
    ```sh
    dotnet add package FivemToolsLib.Server
    ```
   
3. Alternatively, you can download `FivemToolsLib.Client.dll` (or for server side `FivemToolsLib.Server.dll`) from the latest GitHub release, and place it in a folder like `libs/`, and add the following to your `.csproj`:

```xml
<ItemGroup>
    <Reference Include="FivemToolsLib.Client" HintPath="libs/FivemToolsLib.Client.dll"/>
</ItemGroup>
```
