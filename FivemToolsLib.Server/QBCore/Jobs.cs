using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FivemToolsLib.Server.QBCore.Models;
using Newtonsoft.Json;
using static FivemToolsLib.Server.QBCore.Core;

namespace FivemToolsLib.Server.QBCore
{
    public static class Jobs
    {
        /// <summary>
        /// <b>SHARED</b> — 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        public static bool AddJob(string jobName, Job job)
        {            
            var tempGrades = new Dictionary<string, object>();
            
            foreach (var gradesKey in job.Grades.Keys)
            {
                var grade = job.Grades[gradesKey];
                tempGrades.Add(gradesKey.ToString(), new
                {
                    name = grade.Name,
                    payment = grade.Payment
                });
            }
            
            bool result = CoreObject.Functions.AddJob(jobName, new
            {
                label = job.Label,
                defaultDuty = job.DefaultDuty,
                offDutyPay = job.OffDutyPay,
                grades = tempGrades,
            });
            
            return result;
        }

        /// <summary>
        /// <b>SHARED</b> — 
        /// </summary>
        /// <param name="jobs"></param>
        public static void AddJobs(Dictionary<string, Job> jobs)
        {
            jobs.ToList().ForEach(job =>
            {
                var tempGrades = new Dictionary<string, object>();
                
                foreach (var gradesKey in job.Value.Grades.Keys)
                {
                    var grade = job.Value.Grades[gradesKey];
                    tempGrades.Add(gradesKey.ToString(), new
                    {
                        name = grade.Name,
                        payment = grade.Payment
                    });
                }
                
                CoreObject.Functions.AddJob(job.Key, new
                {
                    label = job.Value.Label,
                    defaultDuty = job.Value.DefaultDuty,
                    offDutyPay = job.Value.OffDutyPay,
                    grades = tempGrades,
                });
            });
        }
        
        public static bool UpdateJob(string jobName, Job job)
        {
            var tempGrades = new Dictionary<string, object>();
            
            foreach (var gradesKey in job.Grades.Keys)
            {
                var grade = job.Grades[gradesKey];
                tempGrades.Add(gradesKey.ToString(), new
                {
                    name = grade.Name,
                    payment = grade.Payment
                });
            }
            
            bool result = CoreObject.Functions.UpdateJob(jobName, new
            {
                label = job.Label,
                defaultDuty = job.DefaultDuty,
                offDutyPay = job.OffDutyPay,
                grades = tempGrades,
            });
            
            return result;
        }
        
        public static void RemoveJob(string jobName)
        {
            CoreObject.Functions.RemoveJob(jobName);
        }
        
        public static void RemoveJobs(string[] jobNames)
        {
            jobNames.ToList().ForEach(name => CoreObject.Functions.RemoveJob(name));
        }
        
        /// <summary>
        /// Retrieves a shared job definition from the core object by its name and constructs a corresponding <see cref="Job"/> instance.
        /// </summary>
        /// <param name="jobName">The name (identifier) of the job to retrieve.</param>
        /// <returns>
        /// A <see cref="Job"/> object representing the shared job if found and constructed successfully; otherwise, <c>null</c>.
        /// </returns>
        public static Job GetJob(string jobName)
        {            
            var jobs = CoreObject.Shared.Jobs;

            dynamic sharedJob = ((IDictionary<string, object>)jobs)[jobName];
            
            if (sharedJob == null)
            {
                Debug.WriteLine($"Server: Job '{jobName}' cannot be found");
                return null;
            }
            
            try
            {
                var grades = sharedJob.grades;
                var gradesDict = new Dictionary<int, JobGrade>();

                foreach (var kvp in grades)
                { 
                    var grade = kvp.Value;
                    gradesDict[Convert.ToInt32(kvp.Key)] = new JobGrade(Convert.ToString(grade.name), Convert.ToInt32(grade.payment));
                }
                
                return new Job(sharedJob.label, sharedJob.defaultDuty, sharedJob.offDutyPay, gradesDict);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error constructing job '{jobName}': {ex.Message}");
                return null;
            }
        }
        
        public static Dictionary<string, Job> GetJobs()
        {
            var jobs = (IDictionary<string, object>)CoreObject.Shared.Jobs;
            var result = new Dictionary<string, Job>();
            
            foreach (var kvp in jobs)
            {
                var json = JsonConvert.SerializeObject(kvp.Value);
                var job = JsonConvert.DeserializeObject<Job>(json);

                result[kvp.Key] = job;
            }

            return result;
        }
    }
}