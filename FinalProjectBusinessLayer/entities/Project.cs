using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string  ProjectDescription { get; set; }
        public DateTime DeadLine { get; set; }
        public List<ProjectsMembers> projectsMembers { get; set; }
        public List<Sprint> Sprints { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
    }
}
