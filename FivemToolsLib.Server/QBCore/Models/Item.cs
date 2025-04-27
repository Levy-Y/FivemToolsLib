namespace FivemToolsLib.Server.QBCore.Models
{
    public class Item
    {
        public string Name { get; }
        public string Label { get; }
        public int Weight { get; }
        public string Type { get; }
        public string Image { get; }
        public bool Unique { get; }
        public bool Useable { get; }
        public bool ShouldClose { get; }
        /// <summary>
        /// Not sure of the type `bool` needs research
        /// </summary>
        public bool Combinable { get; }
        public string Description { get; }

        public Item(string name, string label, int weight, string type, string image, bool unique, bool useable, bool shouldClose,
            bool combinable, string description)
        {
            Name = name;
            Label = label;
            Weight = weight;
            Type = type;
            Image = image;
            Unique = unique;
            Useable = useable;
            ShouldClose = shouldClose;
            Combinable = combinable;
            Description = description;
        }
    }
}