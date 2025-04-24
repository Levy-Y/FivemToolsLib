# Client
---  

Once installed, you can begin using the library in your scripts.

### Example: Sending a Notification

```csharp

    using FivemToolsLib.Client.QBCore;

    public class ClientMain : BaseScript 
    { 
        public  MyClientScript()
        { 
            var coreObject = Exports["qb-core"].GetCoreObject(); 
            var qbCore = new FivemToolsLib.Client.QBCore.Client(coreObject);

            qbCore.Notify("My first notification", NotifyTypes.SUCCESS);
        }
    }

```

This will show a QBCore-styled success notification when the script runs.

----------

## 3. Next Steps

- Browse the [API Reference](/api/FivemToolsLib.Client.NativeWrappers.html) for available classes and methods.

- Submit feedback or report issues on [GitHub](https://github.com/YourUsername/FivemToolsLib/issues).

----------

## Troubleshooting

-  **Nothing shows up?** Make sure QBCore is properly initialized and available on the client.

-  **Missing methods or types?** Check your project is referencing the correct version of the DLL.

-  **Target framework mismatch?** Double-check you're using **.NET Framework 4.5.2**, not newer .NET versions like Core or 6+.

----------

# Server
> More examples are coming soon!