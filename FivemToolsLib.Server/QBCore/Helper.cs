using CitizenFX.Core;

namespace FivemToolsLib.Server.QBCore
{
    internal class Helper : BaseScript
    {
        public ExportDictionary GetExportDictionary()
        {
            return Exports;
        }

        public EventHandlerDictionary GetEventHandlerDictionary()
        {
            return EventHandlers;
        }
    }
}