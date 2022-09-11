using AppLayer.Models;
using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppLayer.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ITeamLeaderRepo _teamLeader;

        private readonly IDutyRepo _dutyRepo;
        private readonly IStatusRepo _statusRepo;
        private readonly ITeamMemberRepo _teamMember;
        private readonly IProjectRepo _Project;
        private readonly ISprintRepo _SprintRepo;
        private readonly IWorkReop _workRepo;
        public ProjectController(ISprintRepo sprintRepo, IProjectRepo Project , ITeamLeaderRepo teamLeader, IDutyRepo dutyRepo, IStatusRepo statusRepo , ITeamMemberRepo teamMember , IWorkReop workRepo)
        {

            _Project = Project;
            _teamLeader = teamLeader;
            _dutyRepo = dutyRepo;
            _statusRepo = statusRepo;
            _teamMember = teamMember;
            _SprintRepo = sprintRepo;
            _workRepo = workRepo;
        }
        
        public IActionResult SubmitProject(ProjectDTO ProjectDto)
        {
            
            if (ModelState.IsValid)
            {

                if(DateTime.Compare(ProjectDto.DeadLine , DateTime.Now) == 1)
                {
                    ProjectDto.Ids.Add(ProjectDto.TeamLeaderId);
                    _Project.AddProject(ProjectDto);
                    return RedirectToAction("AddProject", new { isSuccess = true });
                }
                else
                {
                    var ProjectManagerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    ViewBag.ProjectManagerId = ProjectManagerId;
                    ViewBag.TeamLeaders = _teamLeader.GetTeamLeaders();
                    ViewBag.TeamMembers = _teamMember.GetTeamMembers();
                    ViewBag.Statuses = _statusRepo.GetStatuses();
                    ViewBag.IsDeadlineFaild = true;
                    return View(nameof(AddProject));
                }
                
            }
            else
            {
                var ProjectManagerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewBag.ProjectManagerId = ProjectManagerId;
                ViewBag.TeamLeaders = _teamLeader.GetTeamLeaders();
                ViewBag.TeamMembers = _teamMember.GetTeamMembers();
                ViewBag.Statuses = _statusRepo.GetStatuses();
                return View(nameof(AddProject));
            }

        }

        public IActionResult AddProject(bool isSuccess = false , bool IsDeadlineFaild = false)
        {
            var ProjectManagerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.ProjectManagerId = ProjectManagerId;
            ViewBag.IsDeadlineFaild = IsDeadlineFaild;
            ViewBag.TeamLeaders = _teamLeader.GetTeamLeaders();
            ViewBag.TeamMembers = _teamMember.GetTeamMembers();
            ViewBag.Statuses = _statusRepo.GetStatuses();
            ViewBag.isSuccess = isSuccess;

            return View();
        }

        public IActionResult DeleteProject(int ProjectId)
        {
            _Project.DeleteProject(ProjectId);
            return RedirectToAction("PMProjects");
        }
        

        public IActionResult EditProject(int ProjectId)
        {
            var ProjectManagerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.ProjectManagerId = ProjectManagerId;
            ViewBag.Project = _Project.GetProject(ProjectId);
            ViewBag.ProjectMembers = _Project.GetProjectMembers(ProjectId);
            ViewBag.TeamLeaders = _teamLeader.GetTeamLeaders();
            ViewBag.TeamMembers = _teamMember.GetTeamMembers();
            ViewBag.Statuses = _statusRepo.GetStatuses();

            return View();
        }

        public IActionResult SubmitEditProject(ProjectDTO ProjectDto)
        {
            ProjectDto.Ids.Add(ProjectDto.TeamLeaderId);
            _Project.EditProject(ProjectDto);
            return RedirectToAction("PMProjects");
        }
        public IActionResult PMProjects()
        {
            var sprintsNum = 0;
            var dutyNum = 0;
            var workNum = 0;
            var ProjectManagerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.ProjectManagerId = ProjectManagerId;
            var projects = _Project.GetProjectManagerProjects(ProjectManagerId);
            var sprints = _SprintRepo.GetAllSprint();
            var duties = _dutyRepo.GetAllDuties();
            ViewBag.Projects = projects;
            ViewBag.Statuses = _statusRepo.GetStatuses();
       

            foreach (var sprint in sprints)
            {
                var Sprints = projects.FindAll(x => x.ProjectId == sprint.ProjectId).ToList();
               
                sprintsNum += Sprints.Count;

            }
            foreach (var sprint in sprints)
            {
               var dutieslist = duties.FindAll(x => x.SprintId == sprint.SprintId).ToList();
                dutyNum += dutieslist.Count;

            }
            foreach (var duty in duties)
            {
                var works = _workRepo.GetAllWorks().FindAll(x => x.DutyId == duty.DutyId).ToList(); 
                workNum += works.Count;

            }

            ViewBag.SprintsNum = sprintsNum;
                ViewBag.dutyNum = dutyNum;
            ViewBag.WorkNum = workNum;
            return View();
        }

        public IActionResult UpdateProjectStatus(int ProjectId)
        {
            _Project.UpdateProjectStatus(ProjectId);
            return RedirectToAction("MyProjects", "TeamLeader");
        }

      
    }
}
