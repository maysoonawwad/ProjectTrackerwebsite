using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppLayer.Controllers
{
    public class ProjectManagerController : Controller
    {
        private readonly IProjectManagerRepo  _createPM;

        public ProjectManagerController(IProjectManagerRepo createPM)
        {
            _createPM = createPM;
        }
        public IActionResult ProjectManagers()
        {
            ViewBag.ProjectManagers = _createPM.GetProjectManagers();
            ViewBag.Bands = _createPM.GetBands();
            return View();
        }

       
        
    }
}
