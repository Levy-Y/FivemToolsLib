namespace FivemToolsLib.Client.QBCore.Model
{
    /// <summary>
    /// Represents a player's job information.
    /// </summary>
    public class JobData
    {
        /// <summary>Gets the job's internal name.</summary>
        public string Name { get; }
        /// <summary>Gets the job's display label.</summary>
        public string Label { get; }
        /// <summary>Gets the job's payment amount.</summary>
        public int Payment { get; }
        /// <summary>Indicates whether the player is currently on duty.</summary>
        public bool OnDuty { get; }
        /// <summary>Indicates whether the player is the boss of the job.</summary>
        public bool IsBoss { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobData"/> class.
        /// </summary>
        /// <param name="name">Job name.</param>
        /// <param name="label">Job label.</param>
        /// <param name="payment">Job payment.</param>
        /// <param name="onDuty">Whether the player is on duty.</param>
        /// <param name="isBoss">Whether the player is a boss.</param>
        public JobData(string name, string label, int payment, bool onDuty, bool isBoss)
        {
            Name = name;
            Label = label;
            Payment = payment;
            OnDuty = onDuty;
            IsBoss = isBoss;
        }
    }
}