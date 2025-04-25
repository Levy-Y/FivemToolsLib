using System;
using CitizenFX.Core;
using FivemToolsLib.Server.QBCore.Enums;

namespace FivemToolsLib.Server.QBCore
{
    /// <summary>
    /// W.I.P Limited functionality, not all the commands are supported from the QB framework
    /// </summary>
    public class Server
    {
        private readonly dynamic _coreObject;
        private dynamic _qbPlayer;
        
        public Server(dynamic coreObject)
        {
            if (coreObject == null) 
            {
                Debug.WriteLine("Server: Core object is null, QBCore isn't initialized");
                return;
            }
            
            _coreObject = coreObject;
            
            Debug.WriteLine("Successful initialization");
        }

        private void ReFetchPlayer(int source)
        {
            var player = _coreObject.Functions.GetPlayer(source);
            
            if (player == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return;
            }
            
            _qbPlayer = player;
        }
        
        public void UpdatePlayerData(int source)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return;
            }

            _qbPlayer.Functions.UpdatePlayerData();
        }
        
        public bool SetJob(int source, string gangName, int grade)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)_qbPlayer.Functions.SetJob(gangName, grade);
        }
        
        public bool SetGang(int source, string gangName, int grade)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Server: Player cannot be found");
                return false;
            }
            
            return (bool)_qbPlayer.Functions.SetGang(gangName, grade);
        }
        
        public string GetName(int source)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return "";
            }
            
            return (string)_qbPlayer.Functions.GetName();
        }
        
        public bool AddMoney(int source, MoneyTypes type, int amount)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return false;
            }
            
            return (bool)_qbPlayer.Functions.AddMoney(type.ToString().ToLower(), amount);
        }
        
        public bool RemoveMoney(int source, MoneyTypes type, int amount)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return false;
            }
            
            return (bool)_qbPlayer.Functions.RemoveMoney(type.ToString().ToLower(), amount);
        }
        
        public bool SetMoney(int source, MoneyTypes type, int amount)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return false;
            }
            
            return (bool)_qbPlayer.Functions.SetMoney(type.ToString().ToLower(), amount);
        }
        
        public bool GetMoney(int source, MoneyTypes type)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return false;
            }
            
            return (bool)_qbPlayer.Functions.GetMoney(type.ToString().ToLower());
        }
        
        public void Save(int source)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return;
            }
            
            _qbPlayer.Functions.Save();
        }
        
        public void Logout(int source)
        {
            ReFetchPlayer(source);
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return;
            }
            
            _qbPlayer.Functions.Logout();
        }
        
        // TODO: Test dynamic method's return type
        public object GetQbPlayers()
        {
            return _coreObject.Functions.GetQBPlayers();
        }

        public void CreateUsableItem(string itemName, Action action)
        {
            _coreObject.Functions.CreateUsableItem(itemName, action);
        }

        public bool CanUseItem(string itemName)
        {
            return (bool)_coreObject.Functions.CanUseItem(itemName);
        }

        public void UseItem(int source, string itemName)
        {
            _coreObject.Functions.UseItem(source, itemName);
        }

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

        public string GetPermission(int source)
        {
            return (string)_coreObject.Functions.GetPermission(source);
        }

        public bool IsPlayerBanned(int source)
        {
            return (bool)_coreObject.Functions.IsPlayerBanned(source);
        }

        public bool IsLicenseInUse(int source)
        {
            return (bool)_coreObject.Functions.IsLicenseInUse(source);
        }
    }
}