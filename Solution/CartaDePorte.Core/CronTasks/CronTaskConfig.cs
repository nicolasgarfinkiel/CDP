using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Globalization;

namespace CartaDePorte.CronTasks
{
    [XmlRoot("Task")]
    public class CronTaskConfig
    {
        public const string FIXED_TYPE = "Fixed";
        public const string IMMEDIATE_TYPE = "Immediate";

        public const string INTERVAL_TYPE_SECONDS = "Seconds";
        public const string INTERVAL_TYPE_MINUTES = "Minutes";
        public const string INTERVAL_TYPE_HOURS = "Hours";
        

        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("Time")]
        public string Time { get; set; }

        [XmlAttribute("IntervalType")]
        public string IntervalType { get; set; }

        [XmlAttribute("Interval")]
        public int Interval { get; set; }

        [XmlAttribute("Assembly")]
        public string Assembly { get; set; }

        [XmlAttribute("Class")]
        public string Class { get; set; }

        public DateTime? AbsoluteExpiration(bool isFirstExecution)
        {
                DateTime? absoluteExpiration = null;
                if (this.Type.ToLower() == CronTaskConfig.FIXED_TYPE.ToLower())
                {
                    DateTime systemDateTime = DateTime.Now;
                    DateTime taskDateTime = DateTime.Parse(this.Time, CultureInfo.InvariantCulture);
                    if (taskDateTime.Ticks < systemDateTime.Ticks) 
                    {
                        taskDateTime = taskDateTime.AddDays(1);
                    }
                    absoluteExpiration = taskDateTime;
                }
                else if (this.Type.ToLower() == CronTaskConfig.IMMEDIATE_TYPE.ToLower())
                {
                    DateTime systemDateTime = DateTime.Now;
                    

                    DateTime taskDateTime = DateTime.Now.AddSeconds(10);

                    if (!isFirstExecution)
                    {
                    if (this.IntervalType.ToLower()==CronTaskConfig.INTERVAL_TYPE_HOURS.ToLower())
                        taskDateTime = DateTime.Now.AddHours(this.Interval);
                    else if (this.IntervalType.ToLower() == CronTaskConfig.INTERVAL_TYPE_MINUTES.ToLower())
                        taskDateTime = DateTime.Now.AddMinutes(this.Interval);
                    else if (this.IntervalType.ToLower() == CronTaskConfig.INTERVAL_TYPE_SECONDS.ToLower())
                        taskDateTime = DateTime.Now.AddSeconds(this.Interval);
                    }
                    absoluteExpiration = taskDateTime;
                }

                return absoluteExpiration;
        }
    }
}
