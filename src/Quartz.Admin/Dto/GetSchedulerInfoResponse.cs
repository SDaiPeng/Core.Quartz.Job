using Quartz.Core;
using Quartz.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quartz.Admin.Dto
{
    public class GetSchedulerInfoResponse
    {
        public List<TriggerDto> TriggerList { get; set; }
        public SchedulerDto Scheduler { get; set; }
        public JobDto Job { get; set; }
    }
}
