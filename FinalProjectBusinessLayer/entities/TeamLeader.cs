using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
   public class TeamLeader : Member
    {
        public List<Sprint> Sprints { get; set; }
    
    }
}
