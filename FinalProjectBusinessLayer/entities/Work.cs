using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
   public class Work
    {
        public int WorkId { get; set; }
        public string WorkDescription { get; set; }
        public Duty Duty { get; set; }
        public int DutyId { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public string RejectNote { get; set; }
        public List<WorkHistory> WorkHistoryList { get; set; }
        public List<WorkAttachment> WorkAttachments { get; set; }
    }
}
