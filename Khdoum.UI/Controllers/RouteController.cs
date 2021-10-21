using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Controllers
{
    public class RouteController : Controller
    {
        public IActionResult BasicData()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }


    }
}
