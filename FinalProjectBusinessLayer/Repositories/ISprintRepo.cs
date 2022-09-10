using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.Repositories
{
   public interface ISprintRepo
    {
        public void AddSprint(SprintDTO sprintdto );
        public  List<Sprint> GetSprints( int ProjectId);

        public void DeleteSprint(int SprintId);
        public Sprint GetSprint(int SprintId);

        public void EditSprint(SprintDTO sprintDto);
        public void UpdateSprintStatus(int SprintId);
        public bool IsSprintsCompleted(int ProjectId);

        public List<DateTime> GetLastSprintEndDae(int ProjectId);
        public List<Sprint> GetAllSprint();
        public List<SprintDTO> GetProjectManagerSprints(string ProjectManagerId);






    }
}
