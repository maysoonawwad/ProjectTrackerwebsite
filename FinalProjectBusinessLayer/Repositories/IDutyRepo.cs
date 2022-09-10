using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.Repositories
{
    public interface IDutyRepo
    {
      

        public void AddDuty(DutyDTO dutyDto );

        public List<Duty> GetDuties(int SprintId);

        public List<Duty> GetTemMemberDuties(string TeamMemberId);
        public Duty DeleteDuty(int DutyId);
        public Duty GetDuty(int DutyId);
        public void EditDuty(DutyDTO dutyDto);

        public void UpdateDutyStatus(int DutyId);
        public bool IsDutiesCompleted(int SprintId);
        public List<Duty> GetAllDuties();
        public List<DutyDTO> ProjectManagerDuties(string ProjectManagerId);


    }
}
