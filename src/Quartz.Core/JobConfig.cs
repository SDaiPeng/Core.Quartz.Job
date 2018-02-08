using System;
using System.Collections.Generic;
using System.Text;

namespace Quartz.Core
{
    public static class JobConfig
    {
        /// <summary>
        /// 统一JOB名称
        /// </summary>
        public static string JobName = "AbiJob";
        /// <summary>
        /// JOB组名称
        /// </summary>
        public static string JobGroup = "AbiJobGroup";
        /// <summary>
        /// 请求Url key
        /// </summary>
        public static string TriggerJobDataKey = "Url";
    }
}
