using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quartz.Admin.Dto;
using Quartz.Admin.Models;
using Quartz.Core;

namespace Quartz.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly QuartzCore _quartzCore;
        public HomeController()
        {
            _quartzCore = new QuartzCore();
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(new GetSchedulerInfoResponse
            {
                TriggerList = await _quartzCore.GetAllTrigger(),
                Scheduler = _quartzCore.GetSchedulerInfo(),
                Job = await _quartzCore.GetJobInfo()
            });
        }
        public IActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// 新增Trigger
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> AddTrigger(string triggerKey, string url, string cron)
        {
            var res = new ApiResult<string>();
            if (await _quartzCore.IsExistTrigger(triggerKey))
            {
                res.IsSuccess = false;
                res.Msg = "该Trigger已经存在";
            }
            else
                res.IsSuccess = await _quartzCore.AddTrigger(triggerKey, url, cron);
            return Json(res);
        }
        /// <summary>
        /// 更新trigger
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateTrigger(string triggerKey, string url)
        {
            return this.Execute(await _quartzCore.UpdateTrigger(triggerKey, url));
        }
        /// <summary>
        /// 暂停trigger
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> PauseTrigger(string triggerKey)
        {
            return this.Execute(await _quartzCore.PauseTrigger(triggerKey));
        }
        /// <summary>
        /// 重启trigger
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ResumeTrigger(string triggerKey)
        {
            return this.Execute(await _quartzCore.ResumeTrigger(triggerKey));
        }
        /// <summary>
        /// 删除trigger
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DeleteTrigger(string triggerKey)
        {
            return this.Execute(await _quartzCore.DeleteTrigger(triggerKey));
        }
        /// <summary>
        /// 暂停计划
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> PauseScheduler()
        {
            return this.Execute(await _quartzCore.PauseScheduler());
        }
        /// <summary>
        /// 启动计划
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> ResumeScheduler()
        {
            return this.Execute(await _quartzCore.ResumeScheduler());
        }
    }
}
