using CitizenFX.Core.Native;

namespace FivemToolsLib.Client.NativeWrappers
{
    /// <summary>
    /// Provides utility methods to work with ped (pedestrian) entities.
    /// </summary>
    public class Ped
    {
        /// <summary>
        /// Deletes a ped entity from the game world.
        /// </summary>
        /// <param name="pedId">The entity ID of the ped to delete.</param>
        public static void DeletePed(int pedId)
        {
            var id = pedId;
            API.DeletePed(ref id);
        }
    }
}