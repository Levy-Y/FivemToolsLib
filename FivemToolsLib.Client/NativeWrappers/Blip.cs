using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace FivemToolsLib.Client.NativeWrappers
{
    /// <summary>
    /// Provides helper methods to create and manage map blips in the game.
    /// </summary>
    public static class Blip
    {
        /// <summary>
        /// Creates a new blip at the specified world position with a custom name, color, and sprite.
        /// </summary>
        /// <param name="name">The name displayed on the map for the blip.</param>
        /// <param name="color">The numeric color ID for the blip.</param>
        /// <param name="sprite">The numeric sprite ID for the blip icon.</param>
        /// <param name="position">The world position where the blip should appear.</param>
        /// <returns>The created <see cref="CitizenFX.Core.Blip"/> object.</returns>
        public static CitizenFX.Core.Blip CreateBlip(string name, int color, int sprite, Vector3 position) 
        {
            var blip = World.CreateBlip(position);
            API.SetBlipColour(blip.Handle, color);
            API.SetBlipSprite(blip.Handle, sprite);
            API.BeginTextCommandSetBlipName("STRING");
            API.AddTextComponentString(name);
            API.EndTextCommandSetBlipName(blip.Handle);
            
            return blip;
        }
        
        /// <summary>
        /// Creates a blip for an existing entity and assigns it a name, color, and sprite.
        /// </summary>
        /// <param name="entityId">The entity ID to attach the blip to.</param>
        /// <param name="name">The name displayed on the map for the blip.</param>
        /// <param name="color">The numeric color ID for the blip.</param>
        /// <param name="sprite">The numeric sprite ID for the blip icon.</param>
        /// <returns>The handle of the created blip.</returns>
        public static int AddBlipForEntity(int entityId, string name, int color, int sprite) 
        {
            var entityBlip = API.AddBlipForEntity(entityId);
            API.SetBlipColour(entityBlip, color);
            API.SetBlipSprite(entityBlip, sprite);
            API.BeginTextCommandSetBlipName("STRING");
            API.AddTextComponentString(name);
            API.EndTextCommandSetBlipName(entityBlip);
            
            return entityBlip;
        }

        /// <summary>
        /// Removes a previously created blip from the map.
        /// </summary>
        /// <param name="blip">The blip to be removed.</param>
        public static void RemoveBlip(CitizenFX.Core.Blip blip)
        {
            var blipHandle = blip.Handle;
            API.RemoveBlip(ref blipHandle);
        }
    }
}