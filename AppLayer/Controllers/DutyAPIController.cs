using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppLayer.Controllers
{
    public class DutyAPIController : Controller
    {
        private readonly IDutyRepo _dutyRepo;
        private readonly IStatusRepo _statusRepo;
        private readonly ITeamLeaderRepo _teamLeaderProject;
        private readonly ITeamMemberRepo _teamMember;
        private readonly IProjectManagerRepo _projectManager;
        public DutyAPIController(ITeamLeaderRepo teamLeaderProject, IDutyRepo dutyRepo, IStatusRepo statusRepo, ITeamMemberRepo teamMember , IProjectManagerRepo projectManager)
        {


            _teamLeaderProject = teamLeaderProject;
            _dutyRepo = dutyRepo;
            _statusRepo = statusRepo;
            _teamMember = teamMember;
            _projectManager = projectManager;
        }

        //public IActionResult AllDutiesAPI(int SprintId)
        //{
        //    ViewBag.SprintId = SprintId;

        //    return View();
        //}

        public IActionResult AddDuty(int ProjectId, int sprintId, bool IsSuccess = false)
        {

            ViewBag.TeamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.SprintId = sprintId;
            ViewBag.ProjectId = ProjectId;
            var projectManagers = _projectManager.GetProjectManagers();
            ViewBag.Statuses = _statusRepo.GetStatuses();
            ViewBag.Members = _teamMember.GetTeamMembersProject(ProjectId);

            return View();
        }

         public IActionResult AllDuties(int SprintId, int ProjectId)
         { 
        ViewBag.Statuses = _statusRepo.GetStatuses();
        ViewBag.Duties = _dutyRepo.GetDuties(SprintId);
        ViewBag.SprintId = SprintId;
       ViewBag.IsDutyCompleted = _dutyRepo.IsDutiesCompleted(SprintId);

        ViewBag.ProjectId = ProjectId;

         return View();
        }
        [HttpGet]
        public JsonResult GetDutiesAPI(int SprintId)
         {
            var Duties = _dutyRepo.GetDuties(SprintId);

             return Json(Duties.Select(x => new { x.DutyName , x.DutyDescription , x.Status , x.SprintId}));
         }

       


        [HttpPost]
        public IActionResult SubmitDuty(DutyDTO dutyDto, int ProjectId)
        {
            if (ModelState.IsValid)
            {
                _dutyRepo.AddDuty(dutyDto);
                return RedirectToAction("AddDuty", new { IsSuccess = true, sprintId = dutyDto.SprintId, ProjectId = ProjectId });
            }
            else
            {
                ViewBag.TeamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewBag.SprintId = dutyDto.SprintId;
                ViewBag.ProjectId = ProjectId;
                ViewBag.Statuses = _statusRepo.GetStatuses();
                ViewBag.Members = _teamMember.GetTeamMembersProject(ProjectId);

                return View(nameof(AddDuty));
            }

        }

        [HttpDelete]
       
        public IActionResult DeleteDuty(int DutyId)
        {
            _dutyRepo.DeleteDuty(DutyId);
            return View();
        }

        

    }
}
