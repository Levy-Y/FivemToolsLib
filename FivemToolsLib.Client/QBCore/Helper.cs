using CitizenFX.Core;

namespace FivemToolsLib.Client.QBCore
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