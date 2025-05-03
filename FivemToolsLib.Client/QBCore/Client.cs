using System;
using CitizenFX.Core;
using FivemToolsLib.Client.QBCore.Models;
using Prop = FivemToolsLib.Client.QBCore.Models.Prop;

namespace FivemToolsLib.Client.QBCore
{
    /// <summary>
    /// Provides high-level client utilities for interacting with QBCore functions.
    /// </summary>
    public static class Client
    {
        private static dynamic _coreObject;

        /// <summary>
        /// Static constructor for the <see cref="Client"/> class. 
        /// Initializes the QBCore export and event handlers required for client functionality.
        /// 
        /// It retrieves the exports and event handlers using the <see cref="Helper"/> class,
        /// verifies the presence of the "qb-core" export, and attempts to obtain the QBCore core object.
        /// If the export or core object is missing, appropriate debug messages are logged.
        /// 
        /// Also registers an event handler for "QBCore:Client:UpdateObject" to refresh the core object at runtime.
        /// </summary>
        /// <exception cref="Exception">
        /// Thrown if an unexpected error occurs during initialization.
        /// </exception>
        static Client()
        {
            var exports = new Helper().GetExportDictionary();
            var eventHandlers = new Helper().GetEventHandlerDictionary();
            
            try 
            {
                if (exports == null)
                {
                    Debug.WriteLine("Exports is null. Please ensure the server environment is correctly initialized.");
                    return;
                }
        
                if (exports["qb-core"] == null)
                {
                    Debug.WriteLine("qb-core export not found. Is QBCore running?");
                    return;
                }
        
                _coreObject = exports["qb-core"]?.GetCoreObject();
        
                if (_coreObject == null) 
                {
                    Debug.WriteLine("Client: Core object is null, QBCore might not be initialized yet");
                    return;
                }
        
                if (eventHandlers == null)
                {
                    Debug.WriteLine("EventHandlers is null. Please ensure that event handlers are correctly set.");
                    return;
                }
        
                eventHandlers["QBCore:Client:UpdateObject"] += new Action(() =>
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

        /// <summary>
        /// Fetches the player data of the local player
        /// </summary>
        /// <returns>A dynamic object representing the player data</returns>
        private static dynamic FetchPlayerData()
        {
            var player = _coreObject.Functions.GetPlayerData();

            if (player != null) return player;
            Debug.WriteLine("Client: Player data cannot be fetched");
            return null;
        }
        
        /// <summary>
        /// Retrieves basic player data such as name, birthdate, and contact information.
        /// </summary>
        /// <returns>A <see cref="PlayerData"/> object with the character's personal details, or null if not found.</returns>
        public static PlayerData GetPlayerData()
        {
            var qbPlayer = FetchPlayerData();
            
            if (qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return null;
            }

            try
            {
                string name = Convert.ToString(qbPlayer.charinfo.firstname);
                string label = Convert.ToString(qbPlayer.charinfo.lastname);
                string birthdate = Convert.ToString(qbPlayer.charinfo.birthdate);
                bool gender = Convert.ToBoolean(qbPlayer.charinfo.gender);
                string nationality = Convert.ToString(qbPlayer.charinfo.nationality);
                string phone = Convert.ToString(qbPlayer.charinfo.phone);
                string account = Convert.ToString(qbPlayer.charinfo.account);
                
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
        public static JobData GetPlayerJobData()
        {
            var qbPlayer = FetchPlayerData();
            
            if (qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return null;
            }

            try
            {
                var job = qbPlayer.job;

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
        public static GangData GetPlayerGangData()
        {
            var qbPlayer = FetchPlayerData();
            
            if (qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return null;
            }

            
            try
            {
                var gangInfo = qbPlayer.gang;
                
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
        public static Metadata GetPlayerMetadata()
        {
            var qbPlayer = FetchPlayerData();
            
            if (qbPlayer == null)
            {
                Debug.WriteLine("Client: Player cannot be found");
                return null;
            }

            var metadata = qbPlayer.metadata;
            
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
        public static void Notify(string message, NotifyTypes type)
        {
            _coreObject.Functions.Notify(message, type.ToString().ToLower());
        }
        
        /// <summary>
        /// Checks if the player has a specific item in their inventory.
        /// </summary>
        /// <param name="itemName">The name of the item to check for.</param>
        /// <returns>True if the item exists in the inventory, otherwise false.</returns>
        public static bool HasItem(string itemName)
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
        public static void Progressbar(
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
        public static int GetClosestPed(Vector3 position)
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
        public static int GetClosestVehicle(Vector3 position)
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
        public static int GetClosestObject(Vector3 position)
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
        public static string GetPlate(Vehicle vehicle)
        {
            return (string)_coreObject.Functions.GetPlate(vehicle);
        }
    }
}