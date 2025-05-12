# Client
---  

Once installed, you can begin using the library in your scripts.

### Example: Sending a Notification

```csharp

    using static FivemToolsLib.Client.QBCore.Client;

    public class ClientMain : BaseScript 
    { 
        public ClientMain()
        { 
            Notify("My first notification", NotifyTypes.SUCCESS);
        }
    }
```

This will show a QBCore-styled success notification when the script runs.

----------

# Server

### Example: Adding a job programatically (advanced)

```csharp

    using static FivemToolsLib.Server.QBCore.Jobs;

    public class ServerMain : BaseScript 
    { 
        public ServerMain()
        { 
            AddJob("taxi", new Job("Taxi", true, false, new Dictionary<int, JobGrade>
            {
                { 2, new JobGrade("Boss", 1000) },
                { 1, new JobGrade("Manager", 100) },
                { 0, new JobGrade("Driver", 10) },
            }));        
        }
    }
```

This will create a new job `taxi`, with 3 grades: `Boss`, `Manager`, `Driver`. And set their salaries to `1000`, `100`, `10`.

---

## Next Steps

- Browse the [API Reference](/api/FivemToolsLib.Client.NativeWrappers.html) for available classes and methods.

- Submit feedback or report issues on [GitHub](https://github.com/YourUsername/FivemToolsLib/issues).

----------

## Troubleshooting

-  **Nothing shows up?** Make sure QBCore is properly initialized and available on your server.

-  **Missing methods or types?** Check your project is referencing the correct version of the **DLL** / **NuGet package**.

-  **Target framework mismatch?** Double-check you're using **.NET Framework 4.5.2**, not newer .NET versions like Core or 6+.


