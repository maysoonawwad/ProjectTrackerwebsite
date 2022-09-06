using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
   public class ProjectsMembers
    {
        public Member Member { get; set; }
        public string Id { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
