using AppLayer.Models;
using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.entities;
using FinalProjectBusinessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectRepo _createProject;
        private readonly IStatusRepo _statusRepo;
        private readonly ApplicationDbContext _AppContext;
        public HomeController(ILogger<HomeController> logger , IProjectRepo createProject , IStatusRepo statusRepo, ApplicationDbContext appContext)
        {
            _logger = logger;
            _createProject = createProject;
            _statusRepo = statusRepo;
            _AppContext = appContext;
        }
     
       
        public IActionResult Index()
        {
            ViewBag.UsersCount = _AppContext.Users.ToList().Count;

                return View();

            
        }



      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
