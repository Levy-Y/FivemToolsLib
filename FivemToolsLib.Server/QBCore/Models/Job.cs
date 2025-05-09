using System.Collections.Generic;

namespace FivemToolsLib.Server.QBCore.Models
{
    public class Job
    {
        public string Label { get; }
        public bool DefaultDuty { get; }
        public bool OffDutyPay { get; }
        public Dictionary<int, JobGrade> Grades { get; }

        public Job(string label, bool defaultDuty, bool offDutyPay, Dictionary<int, JobGrade> grades)
        {
            Label = label;
            DefaultDuty = defaultDuty;
            OffDutyPay = offDutyPay;
            Grades = grades;
        }
    }

    public class JobGrade
    {
        public string Name { get; }
        public int Payment { get; }

        public JobGrade(string name, int payment)
        {
            Name = name;
            Payment = payment;
        }
    }
}