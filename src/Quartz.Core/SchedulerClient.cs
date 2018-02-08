using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Quartz.Core
{
    public static class SchedulerClient
    {
        public static IScheduler scheduler;
        static SchedulerClient()
        {
            var props = new NameValueCollection
            {
                ["quartz.serializer.type"] = "binary",
                ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",//存储类型
                ["quartz.jobStore.tablePrefix"] = "QRTZ_",//表明前缀
                ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",//驱动类型
                ["quartz.jobStore.dataSource"] = "myDS", //数据源名称c
                ["quartz.dataSource.myDS.connectionString"] = @"Server=172.18.209.187;Database=QuartzJob;User ID=sa;Password=!234Qwer;",//连接字符串
                ["quartz.dataSource.myDS.provider"] = "SqlServer"
            };
            var schedulerFactory = new StdSchedulerFactory(props);
            scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
        }
    }
}
