using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.Repositories
{
   public interface IWorkReop
    {
        public void AddWork(List<IFormFile> fileData , WorkDTO workDto);
        public List<Work> GetWorks(int DutyId);

        public List<WorkAttachment> ShowWorkAttachs(int WorkId);
        public Work GetWork(int WorkId);
        
        public void ApproveWork(WorkDTO workDto);
        public void RejectWork(WorkDTO workDto);
        public void DeleteWork(int WorkId);
        public void EditWork(WorkDTO workDto, List<IFormFile> fileData);

       public List<WorkAttachment> GetFiles(int WorkId);
       // public List<IFormFile> FormFiles(int WorkId);
        public List<WorksHistory> GetWorksHistory(int ProjectId);
        public bool IsWorkCompleted(int DutyId);

        public void DeleteFile(int AttachId);

    }
}
