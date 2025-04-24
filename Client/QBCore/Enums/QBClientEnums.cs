namespace FivemToolsLib.Client.QBCore
{
    /// <summary>
    /// Represents different types of notifications that can be shown to the player.
    /// </summary>
    public enum NotifyTypes
    {
        /// <summary>
        /// Represents an error notification, typically used for failures or critical issues.
        /// </summary>
        ERROR,

        /// <summary>
        /// Represents a success notification, typically used for completed or successful actions.
        /// </summary>
        SUCCESS,

        /// <summary>
        /// Represents a primary notification, used for general informational messages.
        /// </summary>
        PRIMARY,

        /// <summary>
        /// Represents a warning notification, used to alert the player to potential issues.
        /// </summary>
        WARNING
    }
}