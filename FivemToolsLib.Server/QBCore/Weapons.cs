using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FivemToolsLib.Server.QBCore.Models;
using Newtonsoft.Json;
using static FivemToolsLib.Server.Tools.Utils;
using static FivemToolsLib.Server.QBCore.Core;

namespace FivemToolsLib.Server.QBCore
{
    public static class Weapons
    {
        /// <summary>
        /// <b>SHARED</b> — 
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public static void AddWeapon(Weapon weapon)
        {
            var qbWeaponsDict = (IDictionary<string, object>)CoreObject.Shared.Weapons;
            
            if (qbWeaponsDict.ContainsKey(ComputeJoaat(weapon.Name).ToString()))
            { 
                Console.WriteLine($"{weapon.Name} already exists.");
                return;
            }
            
            qbWeaponsDict[weapon.Name] = new
            {
                weapontype = weapon.WeaponType,
                name = weapon.Name,
                damagereason = weapon.DamageReason,
                label = weapon.Label
            };
        }
        
        /// <summary>
        /// <b>SHARED</b> — 
        /// </summary>
        /// <param name="weapons"></param>
        public static void AddWeapons(Weapon[] weapons)
        {
            var qbWeaponsDict = (IDictionary<string, object>)CoreObject.Shared.Weapons;
            
            weapons.ToList().ForEach(weapon =>
            {
                if (qbWeaponsDict.ContainsKey(ComputeJoaat(weapon.Name).ToString()))
                { 
                    Console.WriteLine($"{weapon.Name} already exists.");
                }
                qbWeaponsDict[weapon.Name] = new
                {
                    weapontype = weapon.WeaponType,
                    name = weapon.Name,
                    damagereason = weapon.DamageReason,
                    label = weapon.Label
                };
            });
        }

        /// <summary>
        /// <b>SHARED</b> — 
        /// </summary>
        /// <param name="weaponName"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public static void UpdateWeapon(string weaponName, Weapon weapon)
        {
            var qbWeaponsDict = (IDictionary<string, object>)CoreObject.Shared.Weapons;

            var name = ComputeJoaat(weaponName).ToString();
            
            if (!qbWeaponsDict.ContainsKey(name))
            { 
                Console.WriteLine($"{weaponName} doesn't exist.");
                return;
            }
            
            qbWeaponsDict[name] = new
            {
                weapontype = weapon.WeaponType,
                name = weapon.Name,
                damagereason = weapon.DamageReason,
                label = weapon.Label
            };
        }
        
        public static bool RemoveWeapon(string weaponName)
        {
            var qbWeaponsDict = (IDictionary<string, object>)CoreObject.Shared.Weapons;
            return qbWeaponsDict.Remove(ComputeJoaat(weaponName).ToString());
        }

        public static void RemoveWeapons(string[] weaponNames)
        {
            var qbWeaponsDict = (IDictionary<string, object>)CoreObject.Shared.Weapons;
            weaponNames.ToList().ForEach(name => qbWeaponsDict.Remove(ComputeJoaat(name).ToString()));
        }
        
        /// <summary>
        /// Retrieves a shared weapon definition from the core object by its name and constructs a corresponding <see cref="Weapon"/> instance.
        /// </summary>
        /// <param name="name">The name (identifier) of the weapon to retrieve.</param>
        /// <returns>
        /// A <see cref="Weapon"/> object representing the shared weapon if found and constructed successfully; otherwise, <c>null</c>.
        /// </returns>
        public static Weapon GetWeapon(string name)
        {
            try
            {
                var qbWeaponsDict = (IDictionary<string, object>)CoreObject.Shared.Weapons;

                if (!qbWeaponsDict.TryGetValue(name, out dynamic value)) return null;

                return new Weapon(value.weapontype, value.name, value.damagereason, value.label);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while executing 'GetWeapon': {ex.Message}");
                return null;
            }
        }
        
        public static Dictionary<string, Weapon> GetWeapons()
        {
            var weapons = (IDictionary<string, object>)CoreObject.Shared.Weapons;
            var result = new Dictionary<string, Weapon>();
            
            foreach (var kvp in weapons)
            {
                var json = JsonConvert.SerializeObject(kvp.Value);
                var weapon = JsonConvert.DeserializeObject<Weapon>(json);

                result[kvp.Key] = weapon;
            }

            return result;
        }
    }
}