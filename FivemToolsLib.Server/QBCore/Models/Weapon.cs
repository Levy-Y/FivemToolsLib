namespace FivemToolsLib.Server.QBCore.Models
{
    public class Weapon
    {
        public string WeaponType { get; }
        public string Name { get; }
        public string DamageReason { get; }
        public string Label { get; }

        public Weapon(string weaponType, string name, string damageReason, string label)
        {
            WeaponType = weaponType;
            Name = name;
            DamageReason = damageReason;
            Label = label;
        }
    }
}