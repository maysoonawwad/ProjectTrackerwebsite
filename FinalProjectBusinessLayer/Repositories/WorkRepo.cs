
using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace FinalProjectBusinessLayer.Repositories
{
    public class WorkRepo : IWorkReop
    {
        private readonly ApplicationDbContext _AppContext;
        private readonly IDutyRepo _DutyRepo;
        public WorkRepo(ApplicationDbContext AppContext , IDutyRepo dutyRepo)
        {
            _AppContext = AppContext;
            _DutyRepo = dutyRepo;
        }

        public void AddWork(List<IFormFile> FileData,WorkDTO workDto)
        {
            var work = new Work();

            work.DutyId = workDto.DutyId;
            work.StatusId = 1;
            work.WorkDescription = workDto.WorkDescription;
            work.WorkId = workDto.WorkId;
            _AppContext.Works.Add(work);
            _AppContext.SaveChanges();


            foreach(var file in FileData)
            {
                Stream st = file.OpenReadStream();

                using (BinaryReader br = new BinaryReader(st))
                {
                    var fileByte = br.ReadBytes((int)st.Length);
                    workDto.FileData = fileByte;
                    _AppContext.WorkAttachments.Add(new WorkAttachment()
                    {
                        WorkAttachmentId = workDto.WorkAttachmentId,
                        WorkId = work.WorkId,
                        ContentType = file.ContentType,
                        FileName = file.FileName,
                        FileData = workDto.FileData,
                    });
                    _AppContext.SaveChanges();
                }

            }

            var WorkHistory = new WorkHistory();
            WorkHistory.DateTime = DateTime.Now;
            WorkHistory.StatusId = workDto.StatusId;
            WorkHistory.WorkId = work.WorkId;
            _AppContext.WorkHistories.Add(WorkHistory);
            _AppContext.SaveChanges();


        }

        public void ApproveWork(WorkDTO workDto)
        {
            var result =  _AppContext.Works.SingleOrDefault(x => x.WorkId == workDto.WorkId);

            result.StatusId = 3;
            _AppContext.SaveChanges();

            var WorkHistory = new WorkHistory();
            WorkHistory.DateTime = DateTime.Now;
            WorkHistory.StatusId = result.StatusId;
            WorkHistory.WorkId = workDto.WorkId;
            _AppContext.WorkHistories.Add(WorkHistory);
            _AppContext.SaveChanges();

        }
        public void RejectWork(WorkDTO workDto)
        {
            var result = _AppContext.Works.SingleOrDefault(x => x.WorkId == workDto.WorkId);

            result.StatusId = 4;
            result.RejectNote = workDto.RejectNote;
            _AppContext.SaveChanges();
            var WorkHistory = new WorkHistory();
            WorkHistory.DateTime = DateTime.Now;
            WorkHistory.StatusId = result.StatusId;
            WorkHistory.WorkId = workDto.WorkId;
            _AppContext.WorkHistories.Add(WorkHistory);
            _AppContext.SaveChanges();

        }
        //get work at specific id
        public Work GetWork(int WorkId)
        {
            return _AppContext.Works.SingleOrDefault(x => x.WorkId == WorkId);

        }
        //get all works according to specific duty
        public List<Work> GetWorks(int DutyId)
        {
            return _AppContext.Works.Where(x => x.DutyId == DutyId).ToList();
        }
        //work attachments
        public List<WorkAttachment> ShowWorkAttachs(int WorkId)
        {
            return _AppContext.WorkAttachments.Include(x => x.Work).Where(x => x.WorkId == WorkId).ToList();

        }

        public void DeleteWork(int WorkId)
        {
            var attach = _AppContext.WorkAttachments.Where(x => x.WorkId == WorkId).ToList();
            _AppContext.WorkAttachments.RemoveRange(attach);
            _AppContext.SaveChanges();

            var history = _AppContext.WorkHistories.Where(x => x.WorkId == WorkId).ToList();
            _AppContext.WorkHistories.RemoveRange(history);
            _AppContext.SaveChanges();

            var work = _AppContext.Works.SingleOrDefault(x => x.WorkId == WorkId);
            _AppContext.Works.Remove(work);
            _AppContext.SaveChanges();




        }

        public void EditWork(WorkDTO workDto , List<IFormFile> fileData)
        {
            var work = new Work();

            work.DutyId = workDto.DutyId;
            work.StatusId = workDto.StatusId;
            work.WorkDescription = workDto.WorkDescription;
            work.WorkId = workDto.WorkId;
            _AppContext.Works.Update(work);
            _AppContext.SaveChanges();


            foreach (var file in fileData)
            {
                Stream st = file.OpenReadStream();

                using (BinaryReader br = new BinaryReader(st))
                {
                    var fileByte = br.ReadBytes((int)st.Length);
                    workDto.FileData = fileByte;
                    _AppContext.WorkAttachments.Add(new WorkAttachment()
                    {
                        WorkAttachmentId = workDto.WorkAttachmentId,
                        WorkId = work.WorkId,
                        ContentType = file.ContentType,
                        FileName = file.FileName,
                        FileData = workDto.FileData,
                    });
                    _AppContext.SaveChanges();
                }

            }

            var WorkHistory = new WorkHistory();
            WorkHistory.DateTime = DateTime.Now;
            WorkHistory.StatusId = workDto.StatusId;
            WorkHistory.WorkId = work.WorkId;
            _AppContext.WorkHistories.Add(WorkHistory);
            _AppContext.SaveChanges();
        }

        public List<WorkAttachment> GetFiles(int WorkId)
        {
           return  _AppContext.WorkAttachments.Include(x => x.Work).Where(x => x.WorkId == WorkId).ToList();
           
        }

        
        public List<WorksHistory> GetWorksHistory(int ProjectId)
        {
            List<WorksHistory> Workhistory = new List<WorksHistory>();

            var res2 = _AppContext.Sprints.Where(x => x.ProjectId == ProjectId).Join(_AppContext.Duties, s => s.SprintId, x => x.SprintId, (s, x) => new { dutyId = x.DutyId }).ToList();
            foreach(var res in res2)
            {
                var work =_AppContext.Works.Where(x => x.DutyId == res.dutyId).Join(_AppContext.WorkHistories, s => s.WorkId, x => x.WorkId, (s, x) => new { workId = x.WorkId , statusId = x.StatusId , s.WorkDescription , Date = x.DateTime }).ToList();


                foreach (var workhistory in work)
                {
                    var WorkHistory = new WorksHistory();

                    if(Workhistory.Count == 0)
                    {
                        WorkHistory.WorkId = workhistory.workId;
                        WorkHistory.WorkDescription = workhistory.WorkDescription;

                        WorkHistory.DateStatus = new List<DateStatushistory>()

                        {

                            new DateStatushistory()
                            {
                                WorkId = workhistory.workId,
                                StatusId = workhistory.statusId,
                                DateTime = workhistory.Date
                            }
                        };


                        Workhistory.Add(WorkHistory);


                    }else if(Workhistory.Count > 0)
                    {
                        foreach(var wh in Workhistory.ToList())
                        {
                            if(wh.WorkId == workhistory.workId)
                            {
                                WorkHistory.DateStatus = new List<DateStatushistory>()
                                {
                                    new DateStatushistory()
                                    {
                                          WorkId = workhistory.workId,
                                    StatusId = workhistory.statusId,
                                    DateTime = workhistory.Date
                                    }

                                };
                            
                                foreach(var ds in WorkHistory.DateStatus)
                                {
                                    wh.DateStatus.Add(ds);
                                }

                            }
                            else
                            {
                                WorkHistory.WorkId = workhistory.workId;
                                WorkHistory.WorkDescription = workhistory.WorkDescription;

                                WorkHistory.DateStatus = new List<DateStatushistory>()

                        {

                            new DateStatushistory()
                            {
                                WorkId = workhistory.workId,
                                StatusId = workhistory.statusId,
                                DateTime = workhistory.Date
                            }
                        };


                                Workhistory.Add(WorkHistory);
                            }
                        }

                    }
                   
                }
                   
            }
            return Workhistory;



        }

        public bool IsWorkCompleted(int DutyId)
        {
            var WorkList  = _AppContext.Works.Where(x => x.DutyId == DutyId).Where(x => x.StatusId != 3).ToList();

            if(WorkList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DeleteFile(int AttachId)
        {
            var result = _AppContext.WorkAttachments.SingleOrDefault(x => x.WorkAttachmentId == AttachId);

            _AppContext.WorkAttachments.Remove(result);
            _AppContext.SaveChanges();
        }

        public List<Work> GetAllWorks()
        {
            return _AppContext.Works.ToList();
        }

        public List<WorkDTO> PMWorks(string ProjectManagerId)
        {
            var duties = _DutyRepo.ProjectManagerDuties(ProjectManagerId);
            List<WorkDTO> worksList = new List<WorkDTO>();
            foreach (var duty in duties)
            {
                var works = _AppContext.Works.Where(x => x.DutyId == duty.DutyId).ToList();

                foreach (var work in works)
                {
                    worksList.Add(new WorkDTO()
                    {
                       WorkDescription = work.WorkDescription,
                       StatusId = work.StatusId,
                       
                    });

                }

            }
            return worksList;
        }
    }
   


}
