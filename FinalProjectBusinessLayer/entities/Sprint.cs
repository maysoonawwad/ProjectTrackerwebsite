using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
    public class Sprint
    {
        public int SprintId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public TeamLeader TeamLeader { get; set; }
        public string TeamLeaderId { get; set; }
        public List<Duty> Duties { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }

    }
}
