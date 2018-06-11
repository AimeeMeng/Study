using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcptionWeb.Models;
using System.Web.Mvc;
namespace ExcptionWeb.App_Start
{
    public class MyHandleErrorAttribute:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;//标记已处理异常，不会重定向页面到异常页
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.Write(filterContext.Exception.Message);
        }
    }
}