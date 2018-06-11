using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using ExcptionWeb.App_Start;
namespace ExcptionWeb.Controllers
{
    public class HomeController : BaseController
    {
        //[MyHandleError]
        public ActionResult Index()
        {
            throw new Exception("test");
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            await Task.Delay(100);
            throw new Exception("test");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void ThreadStartMethord()
        {
            throw new Exception("ThreadError");
        }
    }
}