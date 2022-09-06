using FinalProjectBusinessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppLayer.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly IProjectRepo _createProject;
        private readonly ITeamMemberRepo  _createTM;
        private readonly IProjectManagerRepo _createPM;


        public TeamMemberController(IProjectRepo createProject,  ITeamMemberRepo createTM, IProjectManagerRepo createPM)
        {
            _createProject = createProject;
            _createTM = createTM;
            _createPM = createPM;
        }
        public IActionResult TeamMemberProjects()
        {
           var TeamMemberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.projects = _createProject.GetTeamMemberProjects(TeamMemberId);
            return View();
        }

        public IActionResult TeamMembers()
        {
            ViewBag.TeamMembers = _createTM.GetTeamMembers();
            ViewBag.Bands = _createPM.GetBands();

            return View();
        }
    }
}
