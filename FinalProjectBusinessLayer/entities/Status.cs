using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
   public class Status
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public List<Project> Projects { get; set; }
        public List<Sprint> Sprints { get; set; }
        public List<Duty> Duties { get; set; }
        public List<Work> Works { get; set; }

    }
}
