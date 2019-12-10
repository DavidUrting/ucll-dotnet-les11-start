using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Web.Controllers
{
    public class PingPongController : Controller
    {
        public IActionResult Ping()
        {
            return RedirectToAction(nameof(Pong));
        }

        public IActionResult Pong()
        {
            return RedirectToAction(nameof(Ping));
        }
    }
}