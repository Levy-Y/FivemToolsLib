namespace FivemToolsLib.Client.QBCore.Model
{
    /// <summary>
    /// Represents metadata about a player's current state.
    /// </summary>
    public class Metadata
    {
        /// <summary>Gets the player's hunger level.</summary>
        public float? Hunger { get; }
        /// <summary>Gets the player's thirst level.</summary>
        public float? Thirst { get; }
        /// <summary>Gets the player's stress level.</summary>
        public float? Stress { get; }
        /// <summary>Indicates whether the player is dead.</summary>
        public bool? IsDead { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        /// <param name="hunger">Hunger level.</param>
        /// <param name="thirst">Thirst level.</param>
        /// <param name="stress">Stress level.</param>
        /// <param name="isDead">Death status.</param>
        public Metadata(float hunger, float thirst, float stress, bool isDead)
        {
            Hunger = hunger;
            Thirst = thirst;
            Stress = stress;
            IsDead = isDead;
        }
    }
}