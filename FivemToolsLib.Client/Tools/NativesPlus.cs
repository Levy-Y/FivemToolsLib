using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace FivemToolsLib.Client.Tools
{
    public class NativesPlus
    {
        /// <summary>
        /// This function operates very similarly to the native <see cref="API.GetEntityCoords">GetEntityCoords</see> method, but it also returns the heading.
        /// </summary>
        /// <returns>The current entity coordinates and heading in a <see cref="Vector4"/> object.</returns>
        public static Vector4 GetCoords()
        {
            var localPlayer = API.PlayerPedId();

            return new Vector4(API.GetEntityCoords(localPlayer, false), API.GetEntityHeading(localPlayer));
        }
    }
}