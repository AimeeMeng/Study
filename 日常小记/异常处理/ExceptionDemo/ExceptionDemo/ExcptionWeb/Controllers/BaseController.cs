using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExcptionWeb.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnException(ExceptionContext filterContext)
        {
            //base.OnException(filterContext);
            filterContext.ExceptionHandled = true;//标记已处理异常，不会重定向页面到异常页
            filterContext.HttpContext.Response.Write(filterContext.Exception.Message);
        }
    }
}