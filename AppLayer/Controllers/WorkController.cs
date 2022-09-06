using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using FinalProjectBusinessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppLayer.Controllers
{
    public class WorkController : Controller
    {
        private readonly IWorkReop _workRepo;
        private readonly IStatusRepo _statusRepo;

        public WorkController(IWorkReop workRepo , IStatusRepo statusRepo)
        {
            _workRepo = workRepo;
            _statusRepo = statusRepo;
            
        }

        public IActionResult AddWork(int DutyId  , bool IsSuccess = false)
        {
            ViewBag.DutyId = DutyId;
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.Statuses = _statusRepo.GetStatuses();
            return View();
        }

        [HttpPost]
        public IActionResult SubmitWork(List<IFormFile> FileData , WorkDTO workDto )
        {
            if (ModelState.IsValid)
            {
                _workRepo.AddWork(FileData, workDto);
                return RedirectToAction("AddWork" , new { IsSuccess = true, DutyId = workDto.DutyId } );
            }
            else
            {
                ViewBag.DutyId = workDto.DutyId;
                ViewBag.Statuses = _statusRepo.GetStatuses();
                return View("AddWork");
            }
           
        }


        public IActionResult TeamLeaderWorksView(int DutyId)
        {
            ViewBag.Statuses = _statusRepo.GetStatuses();

            ViewBag.worksList = _workRepo.GetWorks(DutyId); 
            return View();
        }

        public IActionResult ShowWork(int WorkId)
        {
            ViewBag.work = _workRepo.GetWork(WorkId);
            ViewBag.SubmitedWork = _workRepo.ShowWorkAttachs(WorkId);
            return View();
        }

        public FileStreamResult GetFile(int FileId , int WorkId)
        {
            var result = _workRepo.ShowWorkAttachs(WorkId);
            var file = result.SingleOrDefault(x => x.WorkAttachmentId == FileId);
            Stream st = new MemoryStream(file.FileData);
            return new FileStreamResult(st, file.ContentType);
        }

        public IActionResult UpdateWorkStatus(WorkDTO workDto)
        {
            _workRepo.ApproveWork(workDto);

            return RedirectToAction("TeamLeaderWorksView" , new { DutyId = workDto.DutyId });
        }
        public IActionResult RejectWork(WorkDTO workDto)
        {
            _workRepo.RejectWork(workDto);


            return RedirectToAction("TeamLeaderWorksView" , new { DutyId = workDto.DutyId });
        }

        public IActionResult ShowDutyWorks(int DutyId)
        {
            ViewBag.DutyId = DutyId;
            ViewBag.IsWorkCompleted = _workRepo.IsWorkCompleted(DutyId);
            ViewBag.worksList = _workRepo.GetWorks(DutyId);
            ViewBag.Statuses = _statusRepo.GetStatuses();
            return View();
        }
       
        public IActionResult EditWorkView(int WorkId)
        {
           ViewBag.WorkId= WorkId;
            
            ViewBag.work = _workRepo.GetWork(WorkId);
            ViewBag.Attachs = _workRepo.GetFiles(WorkId);
            return View();
        }
        [HttpPost]
        public IActionResult SubmitEditWork(WorkDTO workDto , List<IFormFile> FileData)
        {
            _workRepo.EditWork(workDto, FileData);

            return RedirectToAction("ShowDutyWorks" , new { DutyId  = workDto.DutyId });
        }

        public IActionResult DeleteWork(int WorkId , int DutyId)
        {
            _workRepo.DeleteWork(WorkId);
            return RedirectToAction("ShowDutyWorks" , new {DutyId = DutyId });
        }

        public IActionResult WorksHistory(int ProjectId)
        
        {
          ViewBag.WorkHistoryList =  _workRepo.GetWorksHistory(ProjectId);
            ViewBag.Statuses = _statusRepo.GetStatuses();
            return View();
        }
        public IActionResult DeleteFile(int AttachId , int WorkId)
        {
            _workRepo.DeleteFile(AttachId);
            return RedirectToAction(nameof(EditWorkView) , new { WorkId = WorkId } );
        }

    }
}
