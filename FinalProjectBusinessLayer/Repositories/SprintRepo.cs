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
    public class SprintRepo: ISprintRepo
    {
        private readonly ApplicationDbContext _AppContext;
        public SprintRepo(ApplicationDbContext AppContext)
        {
            _AppContext = AppContext;
        }

        public void AddSprint(SprintDTO sprintdto )
        {
            var sprint = new Sprint();
            sprint.StartDate = sprintdto.StartDate;
            sprint.EndDate = sprintdto.EndDate;
            sprint.StatusId = 1;
            sprint.TeamLeaderId = sprintdto.TeamLeaderId;
            sprint.ProjectId = sprintdto.ProjectId;
            _AppContext.Sprints.Add(sprint);
            _AppContext.SaveChanges();
        }

        public void DeleteSprint(int SprintId)
        {
            var Duties = _AppContext.Duties.Where(x => x.SprintId == SprintId).ToList();
            foreach(var duty in Duties)
            {
                 var Works =  _AppContext.Works.Where(x => x.DutyId == duty.DutyId).ToList();
                foreach (var work in Works)
                {
                    var attach = _AppContext.WorkAttachments.Where(x => x.WorkId == work.WorkId).ToList();
                    _AppContext.WorkAttachments.RemoveRange(attach);
                    _AppContext.SaveChanges();

                    var history = _AppContext.WorkHistories.Where(x => x.WorkId == work.WorkId).ToList();
                    _AppContext.WorkHistories.RemoveRange(history);
                    _AppContext.SaveChanges();
                }
                _AppContext.Works.RemoveRange(Works);
                _AppContext.SaveChanges();
          

            }

            _AppContext.Duties.RemoveRange(Duties);
            _AppContext.SaveChanges();
            var sprint = _AppContext.Sprints.SingleOrDefault(x => x.SprintId == SprintId);
            _AppContext.Sprints.Remove(sprint);
            _AppContext.SaveChanges();

        }

        public void EditSprint(SprintDTO sprintdto)
        {
            var sprint = new Sprint();
            sprint.StartDate = sprintdto.StartDate;
            sprint.EndDate = sprintdto.EndDate;
            sprint.StatusId = sprintdto.StatusId;
            sprint.TeamLeaderId = sprintdto.TeamLeaderId;
            sprint.ProjectId = sprintdto.ProjectId;
            sprint.SprintId = sprintdto.SprintId;
            _AppContext.Sprints.Update(sprint);
            _AppContext.SaveChanges();
        }

        public Sprint GetSprint(int SprintId)
        {
           return  _AppContext.Sprints.SingleOrDefault(x => x.SprintId == SprintId);
        }

        public List<Sprint> GetSprints( int ProjectId)
        {
            return _AppContext.Sprints.Include(x => x.Project).Where(s => s.ProjectId == ProjectId).ToList();

        }

        public void UpdateSprintStatus(int SprintId)
        {
            var sprint = _AppContext.Sprints.SingleOrDefault(x => x.SprintId == SprintId);
            sprint.StatusId = 2;
            _AppContext.SaveChanges();
        }
        public bool IsSprintsCompleted(int ProjectId)
        {
            var SprintsList = _AppContext.Sprints.Where(x => x.ProjectId == ProjectId).Where(x => x.StatusId != 2).ToList();

            if (SprintsList.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<DateTime> GetLastSprintEndDae(int ProjectId)
        {
            var result = _AppContext.Sprints.Include(x => x.Project).Where(s => s.ProjectId == ProjectId).OrderBy(s => s.EndDate).Select(s => s.EndDate).ToList();

            return result;


        }
    }
}
