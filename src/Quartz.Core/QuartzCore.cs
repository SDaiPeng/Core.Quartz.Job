using Quartz.Core.Dto;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Core
{
    public class QuartzCore : IDisposable
    {
        private readonly JobKey _jobKey;
        private readonly IScheduler _scheduler;
        public QuartzCore()
        {
            _scheduler = SchedulerClient.scheduler;
            _jobKey = JobKey.Create(JobConfig.JobName, JobConfig.JobGroup);
        }

        public SchedulerDto GetSchedulerInfo()
        {
            return new SchedulerDto
            {
                SchedulerName = _scheduler.SchedulerName,
                InstanceId = _scheduler.SchedulerInstanceId
            };
        }

        public async Task<JobDto> GetJobInfo()
        {
            var job = await _scheduler.GetJobDetail(_jobKey);
            return new JobDto
            {
                JobName = job != null ? job.Key.Name : string.Empty
            };
        }
        public async Task<List<TriggerDto>> GetAllTrigger()
        {
            var res = new List<TriggerDto>();
            var triggers = await _scheduler.GetTriggersOfJob(_jobKey);
            foreach (var item in triggers)
            {
                var triggerState = await _scheduler.GetTriggerState(new TriggerKey(item.Key.Name, TriggerKey.DefaultGroup));
                res.Add(new TriggerDto
                {
                    TriggerStatus = triggerState.ToString(),
                    TriggerName = item.Key.Name,
                    TriggerGroup = item.Key.Group,
                    StartDate = item.StartTimeUtc.DateTime.ToString("yyyy-MM-dd"),
                    EndDate = item.EndTimeUtc.ToDateTimeString(),
                    NextFireDate = item.GetNextFireTimeUtc().ToDateTimeString(),
                    PreviousFireDate = item.GetPreviousFireTimeUtc().ToDateTimeString()
                });
            };
            return res;
        }

        public async Task<bool> AddTrigger(string triggerKey, string url, string cron)
        {
            var jobKey = JobKey.Create(JobConfig.JobName, JobConfig.JobGroup);
            var job = await _scheduler.GetJobDetail(jobKey);
            if (job == null)
            {
                job = JobBuilder.Create<CallingApiService>()
                         .WithIdentity(jobKey)
                         .StoreDurably(true)
                         .Build();
                await _scheduler.AddJob(job, false);
            }
            else
                await _scheduler.ResumeJob(jobKey);
            var trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey, TriggerKey.DefaultGroup)
                .UsingJobData(JobConfig.TriggerJobDataKey, url)
                .StartNow()
                .ForJob(jobKey)
                .WithCronSchedule(cron)
                .Build();

            await _scheduler.ScheduleJob(trigger);
            return true;
        }
        public async Task<bool> UpdateTrigger(string triggerKey, string url)
        {
            var trigger = await _scheduler.GetTrigger(new TriggerKey(triggerKey, TriggerKey.DefaultGroup));
            trigger.GetTriggerBuilder().UsingJobData(JobConfig.TriggerJobDataKey, url);
            await _scheduler.ScheduleJob(trigger);
            return true;
        }

        public async Task<bool> ResumeTrigger(string triggerKey)
        {
            await _scheduler.ResumeTrigger(new TriggerKey(triggerKey));
            return true;
        }

        public async Task<bool> PauseTrigger(string triggerKey)
        {
            await _scheduler.PauseTrigger(new TriggerKey(triggerKey));
            return true;
        }

        public async Task<bool> DeleteTrigger(string triggerKey)
        {
            return await _scheduler.UnscheduleJob(new TriggerKey(triggerKey));
        }

        public async Task<bool> IsExistTrigger(string triggerKey)
        {
            var trigger = await _scheduler.GetTrigger(new TriggerKey(triggerKey));
            if (trigger != null)
                return true;
            return false;
        }

        public async Task<bool> PauseScheduler()
        {
            await _scheduler.PauseAll();
            return true;
        }

        public async Task<bool> ResumeScheduler()
        {
            await _scheduler.ResumeAll();
            return true;
        }

        public void Dispose()
        {
        }
    }
}
