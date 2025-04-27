namespace FivemToolsLib.Client.QBCore.Models
{
    /// <summary>
    /// Represents detailed information about a player's identity.
    /// </summary>
    public class PlayerData
    {
        /// <summary>Gets the player's first name.</summary>
        public string FirstName { get; }
        /// <summary>Gets the player's last name.</summary>
        public string LastName { get; }
        /// <summary>Gets the player's birthdate.</summary>
        public string BirthDate { get; }
        /// <summary>Indicates the player's gender.</summary>
        public bool Gender { get; }
        /// <summary>Gets the player's nationality.</summary>
        public string Nationality { get; }
        /// <summary>Gets the player's phone number.</summary>
        public string Phone { get; }
        /// <summary>Gets the player's account number.</summary>
        public string Account { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerData"/> class.
        /// </summary>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="birthDate">Birth date.</param>
        /// <param name="gender">Gender.</param>
        /// <param name="nationality">Nationality.</param>
        /// <param name="phone">Phone number.</param>
        /// <param name="account">Bank account.</param>
        public PlayerData(string firstName, string lastName, string birthDate, bool gender, string nationality,
            string phone, string account)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
            Nationality = nationality;
            Phone = phone;
            Account = account;
        }
    }
}