using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivemToolsLib.Server.QBCore.Enums;
using static FivemToolsLib.Server.QBCore.Core;
using Debug = System.Diagnostics.Debug;

namespace FivemToolsLib.Server.QBCore
{
    public static class Player
    {
        private static dynamic FetchPlayer(int source)
        {
            var player = CoreObject.Functions.GetPlayer(source);
            
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
        
        public static bool HasItem(int source, string itemName, int amount = 1)
        {
            var player = FetchPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)player.Functions.HasItem(itemName, amount);
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
        
        public static void AddPermission(int source, string permission)
        {            
            CoreObject.Functions.AddPermission(source, permission);
        }

        public static void RemovePermission(int source, string permission)
        {            
            CoreObject.Functions.RemovePermission(source, permission);
        }

        public static bool HasPermission(int source, string permission)
        {            
            return (bool)CoreObject.Functions.HasPermission(source, permission);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Dictionary<string, bool> GetPermission(int source)
        {
            var permissions = CoreObject.Functions.GetPermission(source);

            if (permissions is IDictionary<string, object> dict)
            {
                return dict.ToDictionary(
                    kvp => kvp.Key,
                    kvp => Convert.ToBoolean(kvp.Value)
                );
            }

            return new Dictionary<string, bool>();
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
            CoreObject.Functions.Kick(source, reason, setKickReason, deferrals);
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