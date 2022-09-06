using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
    public class Duty
    {
        public int DutyId { get; set; }
        public string DutyName { get; set; }
        public string DutyDescription { get; set; }
        public TeamMember TeamMember { get; set; }
        public string TeamMemberId { get; set; }
        public Sprint Sprint { get; set; }
        public int SprintId { get; set; }
        public List<Work> Works { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
     

    }
}
