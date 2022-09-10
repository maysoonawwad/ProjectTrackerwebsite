using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProjectBusinessLayer.Repositories
{
    public class DutyRepo : IDutyRepo
    {
        private readonly ApplicationDbContext _AppContext;
        private readonly ISprintRepo _SprintRepo;
        public DutyRepo(ApplicationDbContext AppContext , ISprintRepo sprintRepo)
        {
            _AppContext = AppContext;
            _SprintRepo = sprintRepo;
        }
        public void AddDuty(DutyDTO dutyDto )
        {
            var Duty  = new Duty();
            Duty.DutyName = dutyDto.DutyName;
            Duty.DutyDescription = dutyDto.DutyDescription;
            Duty.TeamMemberId = dutyDto.TeamMemberId;
            Duty.SprintId = dutyDto.SprintId;
            Duty.StatusId = 1;
            _AppContext.Duties.Add(Duty);
            _AppContext.SaveChanges();
               
        }
        public Duty DeleteDuty(int DutyId)
        {
            var works = _AppContext.Works.Where(x => x.DutyId == DutyId).ToList();

            foreach(var work in works)
            {
                var attach = _AppContext.WorkAttachments.Where(x => x.WorkId == work.WorkId).ToList();
                _AppContext.WorkAttachments.RemoveRange(attach);
                _AppContext.SaveChanges();

                var history = _AppContext.WorkHistories.Where(x => x.WorkId == work.WorkId).ToList();
                _AppContext.WorkHistories.RemoveRange(history);
                _AppContext.SaveChanges();
            }
           
            _AppContext.Works.RemoveRange(works);
            _AppContext.SaveChanges();
            
            var Duty = _AppContext.Duties.SingleOrDefault(x => x.DutyId == DutyId);
            
            _AppContext.Duties.Remove(Duty);
            _AppContext.SaveChanges();
            return Duty;

        }
        public void EditDuty(DutyDTO dutyDto)
        {
            var Duty = new Duty();
            Duty.DutyName = dutyDto.DutyName;
            Duty.DutyDescription = dutyDto.DutyDescription;
            Duty.TeamMemberId = dutyDto.TeamMemberId;
            Duty.SprintId = dutyDto.SprintId;
            Duty.StatusId = dutyDto.StatusId;
            Duty.DutyId = dutyDto.DutyId;
            _AppContext.Duties.Update(Duty);
            _AppContext.SaveChanges();
        }
        public List<Duty> GetDuties(int SprintId)
        {
            var duties = _AppContext.Duties.Include(x => x.Sprint).Where(s => s.SprintId == SprintId).ToList();
            return duties;
        }


        public Duty GetDuty(int DutyId)
        {
            return _AppContext.Duties.SingleOrDefault(x => x.DutyId == DutyId);
        }


        public List<Duty> GetTemMemberDuties( string TeamMemberId)
        {

            return _AppContext.Duties.Include(x => x.Sprint).ThenInclude(x => x.Project).Include(x => x.TeamMember).Where(x => x.TeamMemberId == TeamMemberId).ToList();
        
        
        }
        public void UpdateDutyStatus(int DutyId)
        {
            var duty=  _AppContext.Duties.SingleOrDefault(x => x.DutyId == DutyId);
            duty.StatusId = 2;
            _AppContext.SaveChanges();
        }
        public bool IsDutiesCompleted(int SprintId)
        {
            var DutiesList = _AppContext.Duties.Where(x => x.SprintId == SprintId).Where(x => x.StatusId != 2).ToList();

            if (DutiesList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Duty> GetAllDuties()
        {
            return _AppContext.Duties.ToList();
        }

        public List<DutyDTO> ProjectManagerDuties(string ProjectManagerId)
        {
            var sprints = _SprintRepo.GetProjectManagerSprints(ProjectManagerId);
            List<DutyDTO> dutiesList = new List<DutyDTO>();
            foreach (var sprint in sprints)
            {
                var duties = _AppContext.Duties.Where(x => x.SprintId == sprint.SprintId).ToList();

                foreach (var duty in duties)
                {
                    dutiesList.Add(new DutyDTO()
                    {
                        DutyName = duty.DutyName,
                        DutyDescription = duty.DutyDescription,
                        StatusId = duty.StatusId,
                        DutyId = duty.DutyId

                    });

                }

            }
            return dutiesList;
        }
    }
}
