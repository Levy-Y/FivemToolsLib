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
    }
}