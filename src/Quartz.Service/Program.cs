using Quartz.Core;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Quartz.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgram().GetAwaiter().GetResult();
            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }

        private static async Task RunProgram()
        {
            IScheduler scheduler = SchedulerClient.scheduler;
            if (!scheduler.IsStarted)
                await scheduler.Start();
        }
    }
}
