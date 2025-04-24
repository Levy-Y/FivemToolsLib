using CitizenFX.Core.Native;

namespace FivemToolsLib.Client.NativeWrappers
{
    /// <summary>
    /// Provides utility methods for sending notifications to the player.
    /// </summary>
    public class Notify
    {
        /// <summary>
        /// Sends a basic on-screen notification to the player.
        /// </summary>
        /// <param name="text">The text to display in the notification.</param>
        public static void SendNotification(string text)
        {
            API.SetNotificationTextEntry("STRING");
            API.AddTextComponentString(text);
            API.DrawNotification(true, false);
        }
    }
}