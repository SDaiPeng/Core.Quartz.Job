using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Core
{
    public class CallingApiService : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.Trigger.JobDataMap;
            var content = dataMap.GetString(JobConfig.TriggerJobDataKey);
            return Console.Out.WriteLineAsync(content);
        }
    }
}
