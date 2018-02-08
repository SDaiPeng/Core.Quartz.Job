using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quartz.Admin.Models;

namespace Quartz.Admin.Controllers
{
    public class BaseController : Controller
    {
        public JsonResult Execute(bool isSuccess)
        {
            var res = new ApiResult<string>
            {
                IsSuccess = isSuccess
            };
            return Json(res);
        }
    }
}