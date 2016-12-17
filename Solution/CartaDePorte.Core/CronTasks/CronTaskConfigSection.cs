using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Configuration;

namespace CartaDePorte.CronTasks
{
    [XmlRoot("CronTasks")]
    public class CronTaskConfigSection
    {


        [XmlElement("Task")]
        public List<CronTaskConfig> Tasks { get; set; }


        //[XmlElement("Log")]
        //public List<LogConfig> Logs { get; set; }
    }
}
