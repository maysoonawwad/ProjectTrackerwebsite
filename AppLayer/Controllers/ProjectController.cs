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
        private readonly IProjectRepo _createProject;
        public ProjectController( IProjectRepo createProject , ITeamLeaderRepo teamLeader, IDutyRepo dutyRepo, IStatusRepo statusRepo , ITeamMemberRepo teamMember)
        {
          
            _createProject = createProject;
            _teamLeader = teamLeader;
            _dutyRepo = dutyRepo;
            _statusRepo = statusRepo;
            _teamMember = teamMember;
        }
        
        public IActionResult SubmitProject(ProjectDTO ProjectDto)
        {
            
            if (ModelState.IsValid)
            {

                if(DateTime.Compare(ProjectDto.DeadLine , DateTime.Now) == 1)
                {
                    ProjectDto.Ids.Add(ProjectDto.TeamLeaderId);
                    _createProject.AddProject(ProjectDto);
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
            _createProject.DeleteProject(ProjectId);
            return RedirectToAction("PMProjects");
        }
        

        public IActionResult EditProject(int ProjectId)
        {
            var ProjectManagerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.ProjectManagerId = ProjectManagerId;
            ViewBag.Project = _createProject.GetProject(ProjectId);
            ViewBag.ProjectMembers = _createProject.GetProjectMembers(ProjectId);
            ViewBag.TeamLeaders = _teamLeader.GetTeamLeaders();
            ViewBag.TeamMembers = _teamMember.GetTeamMembers();
            ViewBag.Statuses = _statusRepo.GetStatuses();

            return View();
        }

        public IActionResult SubmitEditProject(ProjectDTO ProjectDto)
        {
            ProjectDto.Ids.Add(ProjectDto.TeamLeaderId);
            _createProject.EditProject(ProjectDto);
            return RedirectToAction("PMProjects");
        }
        public IActionResult PMProjects()
        {


            var ProjectManagerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.Projects = _createProject.GetProjectManagerProjects(ProjectManagerId);
            ViewBag.Statuses = _statusRepo.GetStatuses();
            return View();
        }

        public IActionResult UpdateProjectStatus(int ProjectId)
        {
            _createProject.UpdateProjectStatus(ProjectId);
            return RedirectToAction("MyProjects", "TeamLeader");
        }
    }
}
