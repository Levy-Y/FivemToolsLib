using System.Collections.Generic;

namespace FivemToolsLib.Server.QBCore.Models
{
    public class Gang
    {
        public string Label { get; }
        public Dictionary<int, GangGrade> Grades { get; }

        public Gang(string label, Dictionary<int, GangGrade> grades)
        {
            Label = label;
            Grades = grades;
        }
    }
    
    public class GangGrade
    {
        public string Name { get; }

        public GangGrade(string name)
        {
            Name = name;
        }
    }
}