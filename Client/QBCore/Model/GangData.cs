namespace FivemToolsLib.Client.QBCore.Model
{
    /// <summary>
    /// Represents a player's gang information.
    /// </summary>
    public class GangData
    {
        /// <summary>Gets the gang's internal name.</summary>
        public string Name { get; }
        /// <summary>Gets the gang's display label.</summary>
        public string Label { get; }
        /// <summary>Indicates whether the player is the boss of the gang.</summary>
        public bool IsBoss { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GangData"/> class.
        /// </summary>
        /// <param name="name">Gang's internal name.</param>
        /// <param name="label">Gang's display label.</param>
        /// <param name="isBoss">Whether the player is the boss.</param>
        public GangData(string name, string label, bool isBoss)
        {
            Name = name;
            Label = label;
            IsBoss = isBoss;
        }
    }
}