using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivemToolsLib.Server.QBCore.Enums;
using FivemToolsLib.Server.QBCore.Models;

namespace FivemToolsLib.Server.QBCore
{
    /// <summary>
    /// W.I.P Limited functionality, not all the commands are supported from the QB framework
    /// </summary>
    public static class Server
    {
        private static dynamic _coreObject;

        static Server()
        {
            var exports = new Helper().GetExportDictionary();
            var eventHandlers = new Helper().GetEventHandlerDictionary();

            try
            {
                _coreObject = exports["qb-core"].GetCoreObject();

                if (_coreObject == null) 
                {
                    Debug.WriteLine("Server: Core object is null, QBCore isn't initialized");
                    return;
                }

                eventHandlers["QBCore:Server:UpdateObject"] += new Action(() =>
                {
                    try 
                    {
                        _coreObject = exports["qb-core"]?.GetCoreObject();
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
        
        private static dynamic FetchPlayer(int source)
        {
            var player = _coreObject.Functions.GetPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return null;
            }
            
            return player;
        }
        
        public static void UpdatePlayerData(int source)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return;
            }

            player.Functions.UpdatePlayerData();
        }
        
        public static bool SetJob(int source, string jobName, int grade)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.SetJob(jobName, grade);
        }
        
        public static bool SetGang(int source, string gangName, int grade)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.SetGang(gangName, grade);
        }
        
        public static string GetName(int source)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return "";
            }
            
            return (string)player.Functions.GetName();
        }
        
        public static bool AddMoney(int source, MoneyTypes type, int amount)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.AddMoney(type.ToString().ToLower(), amount);
        }
        
        public static bool RemoveMoney(int source, MoneyTypes type, int amount)
        {
            var player = FetchPlayer(source);

            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.RemoveMoney(type.ToString().ToLower(), amount);
        }
        
        public static bool SetMoney(int source, MoneyTypes type, int amount)
        {
            var player = FetchPlayer(source);

            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.SetMoney(type.ToString().ToLower(), amount);
        }
        
        public static bool GetMoney(int source, MoneyTypes type)
        {
            var player = FetchPlayer(source);

            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.GetMoney(type.ToString().ToLower());
        }
        
        public static void Save(int source)
        {
            var player = FetchPlayer(source);

            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return;
            }
            
            player.Functions.Save();
        }
        
        public static void Logout(int source)
        {
            var player = FetchPlayer(source);

            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return;
            }
            
            player.Functions.Logout();
        }
        
        // TODO: Test dynamic method's return type
        public static object GetQbPlayers()
        {            
            return _coreObject.Functions.GetQBPlayers();
        }
        
        // TODO: ExpandoObject should be an Item
        /// <summary>
        /// Registers an item as usable in QBCore
        /// </summary>
        /// <param name="itemName">The name of the item</param>
        /// <param name="action">The action to run when the item is used</param>
        public static void CreateUsableItem(string itemName, Action<int, ExpandoObject> action)
        {            
            _coreObject.Functions.CreateUseableItem(itemName, action);
        }

        /// <summary>
        /// Checks if an item is registered as usable. 
        /// </summary>
        /// <param name="itemName">The name of the item to check for usability.</param>
        /// <returns>True if the item is usable, false otherwise.</returns>
        public static bool CanUseItem(string itemName)
        {            
            return _coreObject.Functions.CanUseItem(itemName) != null;
        }

        /// <summary>
        /// Retrieves a shared item definition from the core object by its name and constructs a corresponding <see cref="Item"/> instance.
        /// </summary>
        /// <param name="itemName">The name (identifier) of the item to retrieve.</param>
        /// <returns>
        /// An <see cref="Item"/> object representing the shared item if found and constructed successfully; otherwise, <c>null</c>.
        /// </returns>
        public static Item GetItem(string itemName)
        {            
            var items = _coreObject.Shared.Items;

            dynamic sharedItem = ((IDictionary<string, object>)items)[itemName];
            
            if (sharedItem == null)
            {
                Debug.WriteLine($"Server: Item '{itemName}' cannot be found");
                return null;
            }
            
            try
            {
                return new Item(sharedItem.name, sharedItem.label, sharedItem.weight, sharedItem.type, sharedItem.image,
                    sharedItem.unique, sharedItem.useable, sharedItem.shouldClose, false,
                    sharedItem.description);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error constructing item '{itemName}': {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Retrieves a shared job definition from the core object by its name and constructs a corresponding <see cref="Job"/> instance.
        /// </summary>
        /// <param name="jobName">The name (identifier) of the job to retrieve.</param>
        /// <returns>
        /// A <see cref="Job"/> object representing the shared job if found and constructed successfully; otherwise, <c>null</c>.
        /// </returns>
        public static Job GetJob(string jobName)
        {            
            var jobs = _coreObject.Shared.Jobs;

            dynamic sharedJob = ((IDictionary<string, object>)jobs)[jobName];
            
            if (sharedJob == null)
            {
                Debug.WriteLine($"Server: Job '{jobName}' cannot be found");
                return null;
            }
            
            try
            {
                var grades = sharedJob.grades;
                var gradesDict = new Dictionary<int, JobGrade>();

                foreach (var kvp in grades)
                { 
                    var grade = kvp.Value;
                    gradesDict[Convert.ToInt32(kvp.Key)] = new JobGrade(Convert.ToString(grade.name), Convert.ToInt32(grade.payment));
                }
                
                return new Job(sharedJob.label, sharedJob.defaultDuty, sharedJob.offDutyPay, gradesDict);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error constructing job '{jobName}': {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Retrieves a shared gang definition from the core object by its name and constructs a corresponding <see cref="Gang"/> instance.
        /// </summary>
        /// <param name="gangName">The name (identifier) of the gang to retrieve.</param>
        /// <returns>
        /// A <see cref="Gang"/> object representing the shared gang if found and constructed successfully; otherwise, <c>null</c>.
        /// </returns>
        public static Gang GetGang(string gangName)
        {            
            var gangs = _coreObject.Shared.Jobs;

            dynamic sharedGang = ((IDictionary<string, object>)gangs)[gangName];
            
            if (sharedGang == null)
            {
                Debug.WriteLine($"Server: Gang '{gangName}' cannot be found");
                return null;
            }
            
            try
            {
                var grades = sharedGang.grades;
                var gradesDict = new Dictionary<int, GangGrade>();

                foreach (var kvp in grades)
                { 
                    var grade = kvp.Value;
                    gradesDict[Convert.ToInt32(kvp.Key)] = new GangGrade(Convert.ToString(grade.name));
                }
                
                return new Gang(sharedGang.label, gradesDict);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error constructing gang '{gangName}': {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="itemName"></param>
        public static void UseItem(int source, string itemName)
        {
            BaseScript.TriggerClientEvent("QBCore:Client:UseItem", source, itemName);
        }

        public static void ShowMe3D(int source, string message)
        {
            BaseScript.TriggerClientEvent("QBCore:Command:ShowMe3D", source, message);
        } 

        // TODO: setKickReason: true causes a qbcore error
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="reason"></param>
        /// <param name="setKickReason"></param>
        /// <param name="deferrals"></param>
        public static void Kick(int source, string reason, bool setKickReason, bool deferrals)
        {            
            _coreObject.Functions.Kick(source, reason, setKickReason, deferrals);
        }

        public static void AddPermission(int source, string permission)
        {            
            _coreObject.Functions.AddPermission(source, permission);
        }

        public static void RemovePermission(int source, string permission)
        {            
            _coreObject.Functions.RemovePermission(source, permission);
        }

        public static bool HasPermission(int source, string permission)
        {            
            return (bool)_coreObject.Functions.HasPermission(source, permission);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Dictionary<string, bool> GetPermission(int source)
        {
            var permissions = _coreObject.Functions.GetPermission(source);

            if (permissions is IDictionary<string, object> dict)
            {
                return dict.ToDictionary(
                    kvp => kvp.Key,
                    kvp => Convert.ToBoolean(kvp.Value)
                );
            }

            return new Dictionary<string, bool>();
        }

        public static bool IsPlayerBanned(int source)
        {            
            return (bool)_coreObject.Functions.IsPlayerBanned(source);
        }

        public static bool IsLicenseInUse(int source)
        {            
            return (bool)_coreObject.Functions.IsLicenseInUse(source);
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
        
        /// <summary>
        /// <b>SHARED</b> — Adds a new entry to the shared items tabel of QBCore
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddItem(string itemName, Item item)
        {            
            bool result = _coreObject.Functions.AddItem(itemName, new
            {
                name = item.Name,
                label = item.Label,
                weight = item.Weight,
                type = item.Type,
                image = item.Image,
                unique = item.Unique,
                useable = item.Useable,
                shouldClose = item.ShouldClose,
                combinable = item.Combinable,
                description = item.Description,
            });
            
            return result;
        }

        /// <summary>
        /// <b>SHARED</b> — 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        public static bool AddJob(string jobName, Job job)
        {            
            var tempGrades = new Dictionary<string, object>();
            
            foreach (var gradesKey in job.Grades.Keys)
            {
                var grade = job.Grades[gradesKey];
                tempGrades.Add(gradesKey.ToString(), new
                {
                    name = grade.Name,
                    payment = grade.Payment
                });
            }
            
            bool result = _coreObject.Functions.AddJob(jobName, new
            {
                label = job.Label,
                defaultDuty = job.DefaultDuty,
                offDutyPay = job.OffDutyPay,
                grades = tempGrades,
            });
            
            return result;
        }
        
        /// <summary>
        /// <b>SHARED</b> — 
        /// </summary>
        /// <param name="gangName"></param>
        /// <param name="gang"></param>
        /// <returns></returns>
        public static bool AddGang(string gangName, Gang gang)
        {            
            var tempGrades = new Dictionary<string, object>();
            
            foreach (var gradesKey in gang.Grades.Keys)
            {
                var grade = gang.Grades[gradesKey];
                tempGrades.Add(gradesKey.ToString(), new
                {
                    name = grade.Name
                });
            }
            
            bool result = _coreObject.Functions.AddGang(gangName, new
            {
                label = gang.Label,
                grades = tempGrades,
            });
            
            return result;
        }
        
        /// <summary>
        /// Sends a notification to a player via the built-in notification system.
        /// </summary>
        /// <param name="source">The player to send the notification to.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="type">The type of notification (success, info, warning, error).</param>
        public static void Notify(int source, string message, NotifyTypes type)
        {
            BaseScript.TriggerClientEvent("QBCore:Notify", source, message, type.ToString().ToLower());
        }    
    }
}