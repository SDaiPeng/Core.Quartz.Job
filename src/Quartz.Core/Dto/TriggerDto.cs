using System;
using System.Collections.Generic;
using System.Text;

namespace Quartz.Core
{
    public class TriggerDto
    {
        public string TriggerStatus { get; set; }
        public string TriggerName { get; set; }
        public string TriggerGroup { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string NextFireDate { get; set; }    
        public string PreviousFireDate { get; set; }
        public string TriggerType { get; set; }
    }
}
