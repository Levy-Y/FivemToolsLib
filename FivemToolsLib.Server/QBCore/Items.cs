using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivemToolsLib.Server.QBCore.Models;
using static FivemToolsLib.Server.QBCore.Core;
using Newtonsoft.Json;
using Debug = System.Diagnostics.Debug;

namespace FivemToolsLib.Server.QBCore
{
    public static class Items
    {
        /// <summary>
        /// <b>SHARED</b> — Adds a new entry to the shared items tabel of QBCore
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddItem(string itemName, Item item)
        {            
            bool result = CoreObject.Functions.AddItem(itemName, new
            {
                name = item.Name,
                label = item.Label,
                weight = item.Weight,
                type = item.Type,
                image = item.Image,
                unique = item.Unique,
                useable = item.Useable,
                shouldClose = item.ShouldClose,
                combinable = item.Combinable,
                description = item.Description,
            });
            
            return result;
        }
        
        /// <summary>
        /// <b>SHARED</b> — Adds a list of new entries to the shared items table of QBCore
        /// </summary>
        /// <param name="items"></param>
        public static void AddItems(Dictionary<string, Item> items)
        {
            items.ToList().ForEach(item =>
            {
                CoreObject.Functions.AddItem(item.Key, new
                {
                    name = item.Value.Name,
                    label = item.Value.Label,
                    weight = item.Value.Weight,
                    type = item.Value.Type,
                    image = item.Value.Image,
                    unique = item.Value.Unique,
                    useable = item.Value.Useable,
                    shouldClose = item.Value.ShouldClose,
                    combinable = item.Value.Combinable,
                    description = item.Value.Description,
                });
            });
        }
        
        public static bool UpdateItem(string itemName, Item item)
        {            
            bool result = CoreObject.Functions.UpdateItem(itemName, new
            {
                name = item.Name,
                label = item.Label,
                weight = item.Weight,
                type = item.Type,
                image = item.Image,
                unique = item.Unique,
                useable = item.Useable,
                shouldClose = item.ShouldClose,
                combinable = item.Combinable,
                description = item.Description,
            });
            
            return result;
        }
        
        public static void RemoveItem(string itemName)
        {
            CoreObject.Functions.RemoveItem(itemName);
        }
        
        public static void RemoveItems(string[] itemNames)
        {
            itemNames.ToList().ForEach(name => CoreObject.Functions.RemoveItem(name));
        }
        
        /// <summary>
        /// Retrieves a shared item definition from the core object by its name and constructs a corresponding <see cref="Item"/> instance.
        /// </summary>
        /// <param name="itemName">The name (identifier) of the item to retrieve.</param>
        /// <returns>
        /// An <see cref="Item"/> object representing the shared item if found and constructed successfully; otherwise, <c>null</c>.
        /// </returns>
        public static Item GetItem(string itemName)
        {            
            var items = CoreObject.Shared.Items;

            dynamic sharedItem = ((IDictionary<string, object>)items)[itemName];
            
            if (sharedItem == null)
            {
                Debug.WriteLine($"Server: Item '{itemName}' cannot be found");
                return null;
            }
            
            try
            {
                return new Item(sharedItem.name, sharedItem.label, sharedItem.weight, sharedItem.type, sharedItem.image,
                    sharedItem.unique, sharedItem.useable, sharedItem.shouldClose, false,
                    sharedItem.description);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error constructing item '{itemName}': {ex.Message}");
                return null;
            }
        }

        public static Dictionary<string, Item> GetItems()
        {
            var items = (IDictionary<string, object>)CoreObject.Shared.Items;
            var result = new Dictionary<string, Item>();
            
            foreach (var kvp in items)
            {
                var json = JsonConvert.SerializeObject(kvp.Value);
                var item = JsonConvert.DeserializeObject<Item>(json);

                result[kvp.Key] = item;
            }

            return result;
        }
        
        // TODO: ExpandoObject should be an Item
        /// <summary>
        /// Registers an item as usable in QBCore
        /// </summary>
        /// <param name="itemName">The name of the item</param>
        /// <param name="action">The action to run when the item is used</param>
        public static void CreateUsableItem(string itemName, Action<int, ExpandoObject> action)
        {            
            CoreObject.Functions.CreateUseableItem(itemName, action);
        }

        /// <summary>
        /// Checks if an item is registered as usable. 
        /// </summary>
        /// <param name="itemName">The name of the item to check for usability.</param>
        /// <returns>True if the item is usable, false otherwise.</returns>
        public static bool CanUseItem(string itemName)
        {            
            return CoreObject.Functions.CanUseItem(itemName) != null;
        }
    }
}