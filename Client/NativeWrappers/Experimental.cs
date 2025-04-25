using CitizenFX.Core.Native;
using FivemToolsLib.Shared;

namespace FivemToolsLib.Client.NativeWrappers
{
    public static class Experimental
    {
        /// <summary>
        /// Creates a blip for an existing entity and assigns it a name, color, and sprite. 
        /// This is an extension method for the existing <see cref="CitizenFX.Core.Ped"/> class.
        /// </summary>
        /// <param name="ped">The ped object</param>
        /// <param name="name">The name displayed on the map for the blip.</param>
        /// <param name="color">The numeric color ID for the blip.</param>
        /// <param name="sprite">The numeric sprite ID for the blip icon.</param>
        /// <returns>The handle of the created blip.</returns>
        public static int AddBlipForEntity(this CitizenFX.Core.Ped ped, string name, int color, int sprite)
        {
            var entityBlip = API.AddBlipForEntity(ped.Handle);
            API.SetBlipColour(entityBlip, color);
            API.SetBlipSprite(entityBlip, sprite);
            API.BeginTextCommandSetBlipName("STRING");
            API.AddTextComponentString(name);
            API.EndTextCommandSetBlipName(entityBlip);

            return entityBlip;
        }
    }
}