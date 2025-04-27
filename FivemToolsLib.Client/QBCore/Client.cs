using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivemToolsLib.Client.QBCore.Models;
using Prop = FivemToolsLib.Client.QBCore.Models.Prop;

namespace FivemToolsLib.Client.QBCore
{
    /// <summary>
    /// Provides high-level client utilities for interacting with QBCore functions.
    /// </summary>
    public class Client
    {
        private readonly dynamic _coreObject;
        private dynamic _qbPlayer;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class with the specified <see cref="BaseScript.Exports"/> object.
        /// </summary>
        /// <param name="exports">The dynamic <see cref="BaseScript.Exports"/> object</param>
        public Client(dynamic exports)
        {
            _coreObject = exports["qb-core"].GetCoreObject();
            
            if (_coreObject == null) 
            {
                Debug.WriteLine("Client: Core object is null, QBCore isn't initialized");
                return;
            }
                
            var player = _coreObject.Functions.GetPlayerData();

            if (player == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return;
            }
            _qbPlayer = player;
            
            Debug.WriteLine("Successful initialization");
        }

        /// <summary>
        /// Refreshes the cached player data from QBCore.
        /// </summary>
        private void ReFetchPlayerData()
        {
            _qbPlayer = _coreObject.Functions.GetPlayerData();
        }

        /// <summary>
        /// Retrieves basic player data such as name, birthdate, and contact information.
        /// </summary>
        /// <returns>A <see cref="PlayerData"/> object with the character's personal details, or null if not found.</returns>
        public PlayerData GetPlayerData()
        {
            ReFetchPlayerData();
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return null;
            }

            try
            {
                string name = Convert.ToString(_qbPlayer.charinfo.firstname);
                string label = Convert.ToString(_qbPlayer.charinfo.lastname);
                string birthdate = Convert.ToString(_qbPlayer.charinfo.birthdate);
                bool gender = Convert.ToBoolean(_qbPlayer.charinfo.gender);
                string nationality = Convert.ToString(_qbPlayer.charinfo.nationality);
                string phone = Convert.ToString(_qbPlayer.charinfo.phone);
                string account = Convert.ToString(_qbPlayer.charinfo.account);
                
                return new PlayerData(name, label, birthdate, gender, nationality, phone, account);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to parse gang data: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Retrieves the player's job data including role, duty status, and wage.
        /// </summary>
        /// <returns>A <see cref="JobData"/> object, or null if data is missing or invalid.</returns>
        public JobData GetPlayerJobData()
        {
            ReFetchPlayerData();
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return null;
            }

            try
            {
                var job = _qbPlayer.job;

                string name = Convert.ToString(job.name);
                string label = Convert.ToString(job.label);
                int payment = Convert.ToInt32(job.payment);
                bool onDuty = Convert.ToBoolean(job.onduty);
                bool isBoss = Convert.ToBoolean(job.isboss);

                return new JobData(name, label, payment, onDuty, isBoss);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to parse job data: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Retrieves the player's gang affiliation data.
        /// </summary>
        /// <returns>A <see cref="GangData"/> object, or null if data is missing or invalid.</returns>
        public GangData GetPlayerGangData()
        {
            ReFetchPlayerData();
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return null;
            }

            
            try
            {
                var gangInfo = _qbPlayer.gang;
                
                string name = Convert.ToString(gangInfo.name);
                string label = Convert.ToString(gangInfo.label);
                bool isBoss = Convert.ToBoolean(gangInfo.isboss);

                return new GangData(name, label, isBoss);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to parse gang data: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Retrieves the player's metadata such as hunger, thirst, stress, and death state.
        /// </summary>
        /// <returns>A <see cref="Metadata"/> object, or null if data is missing or invalid.</returns>
        public Metadata GetPlayerMetadata()
        {
            ReFetchPlayerData();
            
            if (_qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return null;
            }

            var metadata = _qbPlayer.metadata;
            
            try
            {
                float hunger = Convert.ToSingle(metadata.hunger);
                float thirst = Convert.ToSingle(metadata.thirst);
                float stress = Convert.ToSingle(metadata.stress);
                bool isDead = Convert.ToBoolean(metadata.isdead);

                return new Metadata(hunger, thirst, stress, isDead);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to parse metadata: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Sends a notification to the player via QBCore's built-in notification system.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="type">The type of notification (success, info, warning, error).</param>
        public void Notify(string message, NotifyTypes type)
        {
            _coreObject.Functions.Notify(message, type.ToString().ToLower());
        }
        
        /// <summary>
        /// Checks if the player has a specific item in their inventory.
        /// </summary>
        /// <param name="itemName">The name of the item to check for.</param>
        /// <returns>True if the item exists in the inventory, otherwise false.</returns>
        public bool HasItem(string itemName)
        {
            return (bool)_coreObject.Functions.HasItem(itemName);
        }
        
        /// <summary>
        /// Displays a QBCore progress bar with optional animations and props.
        /// </summary>
        /// <param name="name">The internal name of the progress bar.</param>
        /// <param name="label">The visible label to show.</param>
        /// <param name="duration">Duration in milliseconds the bar should run.</param>
        /// <param name="useWhileDead">Whether it can be used while the player is dead.</param>
        /// <param name="canCancel">Whether the action can be canceled.</param>
        /// <param name="controlDisables">Options to disable player controls during progress.</param>
        /// <param name="animation">Optional animation to play during the progress bar.</param>
        /// <param name="prop">Optional prop to attach during the progress bar.</param>
        /// <param name="propTwo">Optional second prop to attach during the progress bar.</param>
        /// <param name="onFinish">Callback to run when progress finishes successfully.</param>
        /// <param name="onCancel">Callback to run if progress is canceled.</param>
        public void Progressbar(
            string name,
            string label,
            int duration,
            bool useWhileDead,
            bool canCancel,
            ControlDisables controlDisables,
            Animation animation = null,
            Prop prop = null,
            Prop propTwo = null,
            Action onFinish = null,
            Action onCancel = null)
        {
            var disablesObj = controlDisables != null
                ? new
                {
                    disableMovement = controlDisables.DisableMovement,
                    disableCarMovement = controlDisables.DisableCarMovement,
                    disableMouse = controlDisables.DisableMouse,
                    disableCombat = controlDisables.DisableCombat,
                }
                : null;
            
            var animObj = animation != null
                ? new
                {
                    animDict = animation.AnimDict,
                    anim = animation.Anim,
                    flags = animation.Flags
                }
                : null;

            var propObj = prop != null
                ? new
                {
                    model = prop.Model,
                    bone = prop.Bone,
                    coords = new { x = prop.Coords.X, y = prop.Coords.Y, z = prop.Coords.Z },
                    rotation = new { x = prop.Rotation.X, y = prop.Rotation.Y, z = prop.Rotation.Z }
                }
                : null;

            var propTwoObj = propTwo != null
                ? new
                {
                    model = propTwo.Model,
                    bone = propTwo.Bone,
                    coords = new { x = propTwo.Coords.X, y = propTwo.Coords.Y, z = propTwo.Coords.Z },
                    rotation = new { x = propTwo.Rotation.X, y = propTwo.Rotation.Y, z = propTwo.Rotation.Z }
                }
                : null;

            _coreObject.Functions.Progressbar(
                name,
                label,
                duration,
                useWhileDead,
                canCancel,
                disablesObj,
                animObj,
                propObj,
                propTwoObj,
                onFinish,
                onCancel);
        }

        /// <summary>
        /// Returns the entity handle of the closest pedestrian to a given position.
        /// </summary>
        /// <param name="position">The position to search from.</param>
        /// <returns>The handle of the closest ped, or 0 if none found.</returns>
        public int GetClosestPed(Vector3 position)
        {
            try
            {
                var entityHandle = _coreObject.Functions.GetClosestPed(position);
                return (int)entityHandle;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetClosestPed: {ex.Message}");
                return 0;
            }
        }
        
        /// <summary>
        /// Returns the entity handle of the closest vehicle to a given position.
        /// </summary>
        /// <param name="position">The position to search from.</param>
        /// <returns>The handle of the closest vehicle, or 0 if none found.</returns>
        public int GetClosestVehicle(Vector3 position)
        {
            try
            {
                var vehicleHandle = _coreObject.Functions.GetClosestVehicle(position);
                return (int)vehicleHandle;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetClosestVehicle: {ex.Message}");
                return 0;
            }
        }
        
        /// <summary>
        /// Returns the entity handle of the closest object to a given position.
        /// </summary>
        /// <param name="position">The position to search from.</param>
        /// <returns>The handle of the closest object, or 0 if none found.</returns>
        public int GetClosestObject(Vector3 position)
        {
            try
            {
                var propHandle = _coreObject.Functions.GetClosestObject(position);
                return (int)propHandle;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetClosestObject: {ex.Message}");
                return 0;
            }
        }
        
        /// <summary>
        /// Retrieves the license plate number from a given vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle entity to get the plate from.</param>
        /// <returns>The license plate string.</returns>
        public string GetPlate(Vehicle vehicle) 
        {
            return (string)_coreObject.Functions.GetPlate(vehicle);
        }
    }
}