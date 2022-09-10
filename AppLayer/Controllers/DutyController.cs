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
    public class DutyController : Controller
    {
        private readonly IDutyRepo _dutyRepo;
        private readonly IStatusRepo _statusRepo;
        private readonly ITeamLeaderRepo _teamLeaderProject;
        private readonly ITeamMemberRepo _teamMember;
        public DutyController(ITeamLeaderRepo teamLeaderProject, IDutyRepo dutyRepo, IStatusRepo statusRepo , ITeamMemberRepo teamMember)
        {

            _teamLeaderProject = teamLeaderProject;
            _dutyRepo = dutyRepo;
            _statusRepo = statusRepo;
            _teamMember = teamMember;
        }

        public IActionResult AddDuty(int ProjectId, int sprintId, bool IsSuccess = false)
        {

            ViewBag.TeamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.SprintId = sprintId;
            ViewBag.ProjectId = ProjectId;
            ViewBag.Statuses = _statusRepo.GetStatuses();
            ViewBag.Members = _teamMember.GetTeamMembersProject(ProjectId);

            return View();
        }
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

        public IActionResult AllDuties(int SprintId, int ProjectId)
        {
            ViewBag.Statuses = _statusRepo.GetStatuses();
            ViewBag.Duties = _dutyRepo.GetDuties(SprintId);
            ViewBag.SprintId = SprintId;
            ViewBag.IsDutyCompleted = _dutyRepo.IsDutiesCompleted(SprintId);

            ViewBag.ProjectId = ProjectId;

            return View();
        }


        public IActionResult TMProjectDuties( int ProjectId )
        {
            ViewBag.ProjectId = ProjectId;
           var TeamMemberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ViewBag.Duties = _dutyRepo.GetTemMemberDuties(TeamMemberId);
            ViewBag.Statuses = _statusRepo.GetStatuses();
            return View();
        }

        public IActionResult DeleteDuty(int DutyId, int ProjectId, int SprintId)
        {
            _dutyRepo.DeleteDuty(DutyId);
            return RedirectToAction("AllDuties" ,new { SprintId = SprintId, ProjectId = ProjectId } );
        }
        public IActionResult EditDuty(int DutyId , int ProjectId)
        {
            ViewBag.Duty = _dutyRepo.GetDuty(DutyId);
            ViewBag.Members = _teamMember.GetTeamMembersProject(ProjectId);
            ViewBag.Statuses = _statusRepo.GetStatuses();
            ViewBag.ProjectId = ProjectId;
            return View();
        }
       
        public IActionResult SubmitEditDuty(DutyDTO DutyDto , int ProjectId)
        {
            if (ModelState.IsValid)
            {
                _dutyRepo.EditDuty(DutyDto);
                return RedirectToAction("AllDuties", new { SprintId = DutyDto.SprintId, ProjectId = ProjectId });

            }
            else
            {

                ViewBag.Duty = _dutyRepo.GetDuty(DutyDto.DutyId);
                ViewBag.Members = _teamMember.GetTeamMembersProject(ProjectId);
                ViewBag.Statuses = _statusRepo.GetStatuses();
                ViewBag.ProjectId = ProjectId;

                return View(nameof(EditDuty));
            }

        }


       public IActionResult UpdateDutyStatus(int DutyId)
        {
            _dutyRepo.UpdateDutyStatus(DutyId);
            return RedirectToAction("ShowDutyWorks", "Work" , new {DutyId = DutyId });
        }

            }
}
