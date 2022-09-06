using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppLayer.Controllers
{
    public class TeamLeaderController : Controller
    {
        private readonly ITeamLeaderRepo _teamLeader;
   
       
        private readonly IStatusRepo _statusRepo;
        private readonly IProjectManagerRepo _createPM;


        public TeamLeaderController(ITeamLeaderRepo teamLeader   , IStatusRepo statusRepo , IProjectManagerRepo createPM)
        {
           
            _teamLeader = teamLeader;
            _createPM = createPM;
            _statusRepo = statusRepo;
        }
        public IActionResult MyProjects()
        {
            var result = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.TeamLeder = result;
            ViewBag.statuses = _statusRepo.GetStatuses();
            ViewBag.projects = _teamLeader.GetTeamLeaderProjects(result);

            return View();
        }

        //public IActionResult AddSprints(string TeamLeaderId , int ProjectId)
        //{
        //    ViewBag.ProjectId = ProjectId;
        //    ViewBag.TeamLeaderId = TeamLeaderId;
        //    ViewBag.statuses = _teamLeaderProject.GetStatuses();
        //    return View();
        //}

        
 

        //public IActionResult SubmitDuty(DutyDTO dutyDto , int sprintId)
        //{

        //    dutyDto.SprintId = sprintId;
        //    _dutyRepo.AddDuty(dutyDto);


        //    return RedirectToAction("AllSprints");
        //}

        public IActionResult TeamLeaders()
        {
            ViewBag.TeamLeaders = _teamLeader.GetTeamLeaders();
            ViewBag.Bands = _createPM.GetBands();
            return View();
        }
    }
}
