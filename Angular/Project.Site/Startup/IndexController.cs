using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Angular4.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Startup/Index.cshtml");
        }
    }
}