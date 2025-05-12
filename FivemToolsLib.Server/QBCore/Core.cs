using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using CitizenFX.Core;
using static FivemToolsLib.Server.Tools.Utils;
using FivemToolsLib.Server.QBCore.Enums;
using FivemToolsLib.Server.QBCore.Models;

namespace FivemToolsLib.Server.QBCore
{
    /// <summary>
    /// W.I.P Limited functionality, not all the commands are supported from the QB framework
    /// </summary>
    public static class Core
    {
        internal static dynamic CoreObject { get; private set; }
        internal static ExportDictionary ExportsDictionary { get; private set; }

        static Core()
        {
            var exports = new Helper().GetExportDictionary();
            var eventHandlers = new Helper().GetEventHandlerDictionary();

            try
            {
                ExportsDictionary = exports;
                CoreObject = exports["qb-core"].GetCoreObject();

                if (CoreObject == null) 
                {
                    Debug.WriteLine("Server: Core object is null, QBCore isn't initialized");
                    return;
                }

                eventHandlers["QBCore:Server:UpdateObject"] += new Action(() =>
                {
                    try 
                    {
                        CoreObject = exports["qb-core"]?.GetCoreObject();
                        Debug.WriteLine("Debug: Refreshed core object");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error refreshing core object: {ex.Message}");
                    }
                });
                
                Debug.WriteLine("Successful initialization");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Client initialization failed: {ex}");
                throw;
            }
            
        }
        
        // TODO: Test dynamic method's return type
        public static object GetQbPlayers()
        {            
            return CoreObject.Functions.GetQBPlayers();
        }

        public static bool IsPlayerBanned(int source)
        {            
            return (bool)CoreObject.Functions.IsPlayerBanned(source);
        }

        public static bool IsLicenseInUse(int source)
        {            
            return (bool)CoreObject.Functions.IsLicenseInUse(source);
        }

        /// <summary>
        /// <b>GLOBAL</b> — All the players on the server will receive this
        /// </summary>
        /// <param name="message"></param>
        /// <param name="position"></param>
        public static void DrawGlobalText(string message, Positions position)
        {
            BaseScript.TriggerClientEvent("qb-core:client:DrawText", message, position.ToString().ToLower());
        }
        
        /// <summary>
        /// <b>GLOBAL</b> — All the players on the server will receive this
        /// </summary>
        /// <param name="message"></param>
        /// <param name="position"></param>
        public static void ChangeGlobalText(string message, Positions position)
        {
            BaseScript.TriggerClientEvent("qb-core:client:ChangeText", message, position.ToString().ToLower());
        }
        
        /// <summary>
        /// <b>GLOBAL</b> — All the players on the server will receive this
        /// </summary>
        public static void HideGlobalText()
        {
            BaseScript.TriggerClientEvent("qb-core:client:HideText");
        }
    }
}