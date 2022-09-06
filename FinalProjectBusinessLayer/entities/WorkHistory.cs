using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
   public class WorkHistory
    {
        public int WorkHistoryId { get; set; }
        public DateTime DateTime { get; set; }
        
        public Work Work { get; set; }
        public int WorkId { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
    }
}
