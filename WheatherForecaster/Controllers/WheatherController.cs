using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WheatherForecaster.Controllers
{
    public class WheatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}