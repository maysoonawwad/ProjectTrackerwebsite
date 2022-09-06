using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
   public class TeamMember : Member
    {

        public List<Duty> Duties { get; set; }
    }
}
