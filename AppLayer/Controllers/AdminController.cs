using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using FinalProjectBusinessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLayer.Controllers
{
    public class AdminController : Controller

    {
        private readonly IProjectManagerRepo _createPM;
        private readonly ITeamLeaderRepo _createTL;
        private readonly ITeamMemberRepo _createTM;

        private readonly ApplicationDbContext _AppDbCtxt;
    
        public AdminController(IProjectManagerRepo createPM, ITeamLeaderRepo createTL, ApplicationDbContext AppDbCtxt , ITeamMemberRepo createTM)
        {
            _createPM = createPM;
            _createTL = createTL;
            _createTM = createTM;
            _AppDbCtxt = AppDbCtxt;
        }

      
        
        [Authorize(Roles = "ADMIN")]
        public IActionResult AddEmployee()
        {
            return View();
        }

        public IActionResult AddPMView()
        {
            ViewBag.bands = _AppDbCtxt.Bands.ToList();
            return View();
        }

        public IActionResult AddTLView()
        {
            ViewBag.bands = _AppDbCtxt.Bands.ToList();
            return View();
        }

        public IActionResult AddTMView()
        {
            ViewBag.bands = _AppDbCtxt.Bands.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterProjectManager(RegisterViewModel model)
        {
            
            
            if (ModelState.IsValid)
            {
                var result = await _createPM.RegisterProjectManager(model);

              
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }

                }


                return RedirectToAction("ProjectManagers" , "ProjectManager");
            }
            else
            {
                ViewBag.bands = _AppDbCtxt.Bands.ToList();
                return View("AddPMView");
            }

          

        }

        public async Task<IActionResult> RegisterTeamLeader(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _createTL.RegisterTeamLeader(model);


                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }

                }
                return RedirectToAction("TeamLeaders", "TeamLeader");



            }
            else
            {
                ViewBag.bands = _AppDbCtxt.Bands.ToList();
                return View("AddTLView");
            }


        }
          public async Task<IActionResult> RegisterTeamMember(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _createTM.RegisterTeamMember(model);


                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }

                }


                return RedirectToAction("TeamMembers", "TeamMember");

            }
            else
            {
                ViewBag.bands = _AppDbCtxt.Bands.ToList();
                return View("AddTMView");
            }



        }
    }
}
