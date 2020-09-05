using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHeroTrainer.Core.Identity;
using SuperHeroTrainer.Core.Repository;
using SuperHeroTrainer.Models;


namespace SuperHeroTrainer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

    }
}