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
    public class SprintController : Controller
    {

        private readonly ISprintRepo _SprintRepo;

        private readonly IStatusRepo _statusRepo;
        private readonly IProjectRepo _projectRepo;

        public SprintController(ISprintRepo sprintRepo, IStatusRepo statusRepo , IProjectRepo projectRepo)
        {
            _SprintRepo = sprintRepo;
            _statusRepo = statusRepo;
            _projectRepo = projectRepo;
          
        }

        public IActionResult AddSprints(int ProjectId, bool IsSuccess = false , bool IsFaild = false, bool IsSprintRangeDateFaild = false, bool IsStartDateFaild = false ,bool IsNotValidSprintDaterange=false)
        {

            string TeamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.ProjectId = ProjectId;
            ViewBag.TeamLeaderId = TeamLeaderId;
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.IsFaild = IsFaild;
            ViewBag.IsSprintRangeDateFaild = IsSprintRangeDateFaild;
            ViewBag.IsStartDateFaild = IsStartDateFaild;
            ViewBag.IsNotValidSprintDaterange = IsNotValidSprintDaterange;
            ViewBag.statuses = _statusRepo.GetStatuses();
            return View();
        }
        public IActionResult SubmitSprint(SprintDTO sprintdto)
        {

            var dates = _SprintRepo.GetLastSprintEndDae(sprintdto.ProjectId);
            if (ModelState.IsValid)
            {
                var sprints = _SprintRepo.GetSprints(sprintdto.ProjectId);
                var deadline = _projectRepo.GetProjectDeadLine(sprintdto.ProjectId);
              
                 foreach(var date in dates)
                {
                    if (DateTime.Compare(sprintdto.StartDate, date) == 1)
                    {
                    }
                    else
                    {
                        ViewBag.ProjectId = sprintdto.ProjectId;
                        ViewBag.TeamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        ViewBag.statuses = _statusRepo.GetStatuses();
                        ViewBag.IsNotValidSprintDaterange = true;
                        return View("AddSprints");
                    }

                }


                if (DateTime.Compare(sprintdto.StartDate, DateTime.Now) == 1)
                {
                    if (DateTime.Compare(sprintdto.EndDate, sprintdto.StartDate) == 1)
                    {
                        if (DateTime.Compare(deadline, sprintdto.StartDate) == 1 && DateTime.Compare(deadline, sprintdto.EndDate) == 1)
                        {
                            _SprintRepo.AddSprint(sprintdto);
                            return RedirectToAction("AddSprints", new { IsSuccess = true, ProjectId = sprintdto.ProjectId });
                        }
                        else
                        {
                            ViewBag.ProjectId = sprintdto.ProjectId;
                            ViewBag.TeamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                            ViewBag.statuses = _statusRepo.GetStatuses();
                            ViewBag.IsFaild = true;
                            return View("AddSprints");
                        }

                    }
                    else
                    {
                        ViewBag.ProjectId = sprintdto.ProjectId;
                        ViewBag.TeamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        ViewBag.statuses = _statusRepo.GetStatuses();
                        ViewBag.IsSprintRangeDateFaild = true;
                        return View("AddSprints");
                    }

                }
                else
                {
                    ViewBag.ProjectId = sprintdto.ProjectId;
                    ViewBag.TeamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    ViewBag.statuses = _statusRepo.GetStatuses();
                    ViewBag.IsStartDateFaild = true;
                    return View("AddSprints");
                }
           }else
            {
                ViewBag.ProjectId = sprintdto.ProjectId;
                ViewBag.TeamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewBag.statuses = _statusRepo.GetStatuses();
                return View("AddSprints");
            }

           
        }
        
        public IActionResult AllSprints( int ProjectId)
        {
            ViewBag.IsSprintsCompleted = _SprintRepo.IsSprintsCompleted(ProjectId);
            ViewBag.Statuses = _statusRepo.GetStatuses();
            ViewBag.ProjectId = ProjectId;
            ViewBag.sprints = _SprintRepo.GetSprints(ProjectId);
            return View();
        }

       
        public IActionResult DeleteSprint(int SprintId , int ProjectId)
        {
            _SprintRepo.DeleteSprint(SprintId);
            return RedirectToAction("AllSprints" , new { ProjectId = ProjectId});
        }
        public IActionResult EditSprint(int SprintId, bool IsFaild = false, bool IsSprintRangeDateFaild = false, bool IsStartDateFaild = false, bool IsNotValidSprintDaterange = false)
        {
            ViewBag.Sprint = _SprintRepo.GetSprint(SprintId);
            ViewBag.statuses = _statusRepo.GetStatuses();

            return View();
        }

        public IActionResult SubmitEditSprint(SprintDTO sprintDto)

        {

            if (ModelState.IsValid)
            {
                var sprints = _SprintRepo.GetSprints(sprintDto.ProjectId);
                var deadline = _projectRepo.GetProjectDeadLine(sprintDto.ProjectId);
                if (DateTime.Compare(sprintDto.StartDate, DateTime.Now) == 1)
                {
                    if (DateTime.Compare(sprintDto.EndDate, sprintDto.StartDate) == 1)
                    {
                        if (DateTime.Compare(deadline, sprintDto.StartDate) == 1 && DateTime.Compare(deadline, sprintDto.EndDate) == 1)
                        {
                            _SprintRepo.EditSprint(sprintDto);
                            return RedirectToAction("AllSprint", new { ProjectId = sprintDto.ProjectId });
                        }
                        else
                        {
                            ViewBag.Sprint = _SprintRepo.GetSprint(sprintDto.SprintId);
                            ViewBag.statuses = _statusRepo.GetStatuses();
                            ViewBag.IsFaild = true;
                            return View("EditSprint");
                        }
                    }
                    else
                    {
                        ViewBag.Sprint = _SprintRepo.GetSprint(sprintDto.SprintId);
                        ViewBag.statuses = _statusRepo.GetStatuses();
                        ViewBag.IsSprintRangeDateFaild = true;
                        return View("EditSprint");
                    }
                }
                else
                {
                    ViewBag.Sprint = _SprintRepo.GetSprint(sprintDto.SprintId);
                    ViewBag.statuses = _statusRepo.GetStatuses();
                    ViewBag.IsStartDateFaild = true;
                    return View("EditSprint");
                }                          

            }
            else
            {
                ViewBag.Sprint = _SprintRepo.GetSprint(sprintDto.SprintId);
                ViewBag.statuses = _statusRepo.GetStatuses();

                return View("EditSprint");
            }


        }

        public IActionResult UpdateSprintStatus(int SprintId , int ProjectId)
        {
            _SprintRepo.UpdateSprintStatus(SprintId);
            return RedirectToAction(nameof(AllSprints) , new {ProjectId = ProjectId } );
        }
        public IActionResult ProjectManagerSprints(string ProjectManagerId)
        {
            ViewBag.Sprints = _SprintRepo.GetProjectManagerSprints(ProjectManagerId);
            ViewBag.Statuses = _statusRepo.GetStatuses();
            return View();
        }
    }
}
