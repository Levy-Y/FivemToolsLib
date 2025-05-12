using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FivemToolsLib.Server.QBCore.Models;
using Newtonsoft.Json;
using static FivemToolsLib.Server.QBCore.Core;

namespace FivemToolsLib.Server.QBCore
{
    public static class Gangs
    {
        /// <summary>
        /// <b>SHARED</b> — 
        /// </summary>
        /// <param name="gangName"></param>
        /// <param name="gang"></param>
        /// <returns></returns>
        public static bool AddGang(string gangName, Gang gang)
        {            
            var tempGrades = new Dictionary<string, object>();
            
            foreach (var gradesKey in gang.Grades.Keys)
            {
                var grade = gang.Grades[gradesKey];
                tempGrades.Add(gradesKey.ToString(), new
                {
                    name = grade.Name
                });
            }
            
            bool result = CoreObject.Functions.AddGang(gangName, new
            {
                label = gang.Label,
                grades = tempGrades,
            });
            
            return result;
        }
        
        /// <summary>
        /// <b>SHARED</b> — 
        /// </summary>
        /// <param name="gangs"></param>
        public static void AddGangs(Dictionary<string, Gang> gangs)
        {
            gangs.ToList().ForEach(gang =>
            {
                var tempGrades = new Dictionary<string, object>();
                
                foreach (var gradesKey in gang.Value.Grades.Keys)
                {
                    var grade = gang.Value.Grades[gradesKey];
                    tempGrades.Add(gradesKey.ToString(), new
                    {
                        name = grade.Name
                    });
                }
                
                CoreObject.Functions.AddGang(gang.Key, new
                {
                    label = gang.Value.Label,
                    grades = tempGrades,
                });
                
            });
        }
        
        public static bool UpdateGang(string gangName, Gang gang)
        {            
            var tempGrades = new Dictionary<string, object>();
            
            foreach (var gradesKey in gang.Grades.Keys)
            {
                var grade = gang.Grades[gradesKey];
                tempGrades.Add(gradesKey.ToString(), new
                {
                    name = grade.Name
                });
            }
            
            bool result = CoreObject.Functions.UpdateGang(gangName, new
            {
                label = gang.Label,
                grades = tempGrades,
            });
            
            return result;
        }
        
        public static void RemoveGang(string gangName)
        {
            CoreObject.Functions.RemoveGang(gangName);
        }
        
        public static void RemoveGangs(string[] gangNames)
        {
            gangNames.ToList().ForEach(name => CoreObject.Functions.RemoveGang(name));
        }
        
        /// <summary>
        /// Retrieves a shared gang definition from the core object by its name and constructs a corresponding <see cref="Gang"/> instance.
        /// </summary>
        /// <param name="gangName">The name (identifier) of the gang to retrieve.</param>
        /// <returns>
        /// A <see cref="Gang"/> object representing the shared gang if found and constructed successfully; otherwise, <c>null</c>.
        /// </returns>
        public static Gang GetGang(string gangName)
        {            
            var gangs = CoreObject.Shared.Jobs;

            dynamic sharedGang = ((IDictionary<string, object>)gangs)[gangName];
            
            if (sharedGang == null)
            {
                Debug.WriteLine($"Server: Gang '{gangName}' cannot be found");
                return null;
            }
            
            try
            {
                var grades = sharedGang.grades;
                var gradesDict = new Dictionary<int, GangGrade>();

                foreach (var kvp in grades)
                { 
                    var grade = kvp.Value;
                    gradesDict[Convert.ToInt32(kvp.Key)] = new GangGrade(Convert.ToString(grade.name));
                }
                
                return new Gang(sharedGang.label, gradesDict);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error constructing gang '{gangName}': {ex.Message}");
                return null;
            }
        }
        
        public static Dictionary<string, Gang> GetGangs()
        {
            var gangs = (IDictionary<string, object>)CoreObject.Shared.Gangs;
            var result = new Dictionary<string, Gang>();
            
            foreach (var kvp in gangs)
            {
                var json = JsonConvert.SerializeObject(kvp.Value);
                var gang = JsonConvert.DeserializeObject<Gang>(json);

                result[kvp.Key] = gang;
            }

            return result;
        }
    }
}