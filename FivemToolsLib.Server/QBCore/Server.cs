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
    public class Server
    {
        private dynamic _coreObject;

        public Server(dynamic exports, EventHandlerDictionary eventHandlers)
        {
            _coreObject = exports["qb-core"].GetCoreObject();

            if (_coreObject == null) 
            {
                Debug.WriteLine("Server: Core object is null, QBCore isn't initialized");
                return;
            }

            eventHandlers["QBCore:Server:UpdateObject"] += new Action(() =>
            {
                _coreObject = exports["qb-core"].GetCoreObject();
                
                Debug.WriteLine("Debug: Refreshed core object");
            });
            
            Debug.WriteLine("Successful initialization");
        }
        
        private dynamic FetchPlayer(int source)
        {
            var player = _coreObject.Functions.GetPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return null;
            }
            
            return player;
        }
        
        public void UpdatePlayerData(int source)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return;
            }

            player.Functions.UpdatePlayerData();
        }
        
        public bool SetJob(int source, string jobName, int grade)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.SetJob(jobName, grade);
        }
        
        public bool SetGang(int source, string gangName, int grade)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.SetGang(gangName, grade);
        }
        
        public string GetName(int source)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return "";
            }
            
            return (string)player.Functions.GetName();
        }
        
        public bool AddMoney(int source, MoneyTypes type, int amount)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.AddMoney(type.ToString().ToLower(), amount);
        }
        
        public bool RemoveMoney(int source, MoneyTypes type, int amount)
        {
            var player = FetchPlayer(source);

            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.RemoveMoney(type.ToString().ToLower(), amount);
        }
        
        public bool SetMoney(int source, MoneyTypes type, int amount)
        {
            var player = FetchPlayer(source);

            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.SetMoney(type.ToString().ToLower(), amount);
        }
        
        public bool GetMoney(int source, MoneyTypes type)
        {
            var player = FetchPlayer(source);

            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.GetMoney(type.ToString().ToLower());
        }
        
        public void Save(int source)
        {
            var player = FetchPlayer(source);

            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return;
            }
            
            player.Functions.Save();
        }
        
        public void Logout(int source)
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
        public object GetQbPlayers()
        {            
            return _coreObject.Functions.GetQBPlayers();
        }
        
        // TODO: ExpandoObject should be an Item
        /// <summary>
        /// Registers an item as usable in QBCore
        /// </summary>
        /// <param name="itemName">The name of the item</param>
        /// <param name="action">The action to run when the item is used</param>
        public void CreateUsableItem(string itemName, Action<int, ExpandoObject> action)
        {            
            _coreObject.Functions.CreateUseableItem(itemName, action);
        }

        /// <summary>
        /// Checks if an item is registered as usable. 
        /// </summary>
        /// <param name="itemName">The name of the item to check for usability.</param>
        /// <returns>True if the item is usable, false otherwise.</returns>
        public bool CanUseItem(string itemName)
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
        public Item GetItem(string itemName)
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

        // TODO: Not working, search for a valid fix!
        /// <summary>
        /// INVALID METHOD — Not working at the moment!
        /// </summary>
        /// <param name="source"></param>
        /// <param name="itemName"></param>
        public void UseItem(int source, string itemName)
        {            
            _coreObject.Functions.UseItem(source, itemName);
        }

        // TODO: setKickReason: true causes a qbcore error
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="reason"></param>
        /// <param name="setKickReason"></param>
        /// <param name="deferrals"></param>
        public void Kick(int source, string reason, bool setKickReason, bool deferrals)
        {            
            _coreObject.Functions.Kick(source, reason, setKickReason, deferrals);
        }

        public void AddPermission(int source, string permission)
        {            
            _coreObject.Functions.AddPermission(source, permission);
        }

        public void RemovePermission(int source, string permission)
        {            
            _coreObject.Functions.RemovePermission(source, permission);
        }

        public bool HasPermission(int source, string permission)
        {            
            return (bool)_coreObject.Functions.HasPermission(source, permission);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Dictionary<string, bool> GetPermission(int source)
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

        public bool IsPlayerBanned(int source)
        {            
            return (bool)_coreObject.Functions.IsPlayerBanned(source);
        }

        public bool IsLicenseInUse(int source)
        {            
            return (bool)_coreObject.Functions.IsLicenseInUse(source);
        }
        
        /// <summary>
        /// <b>SHARED</b> — Adds a new entry to the shared items tabel of QBCore
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddItem(string itemName, Item item)
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
        public bool AddJob(string jobName, Job job)
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
        public bool AddGang(string gangName, Gang gang)
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
    }
}