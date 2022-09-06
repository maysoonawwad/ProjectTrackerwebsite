using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.Repositories
{
    public interface IProjectRepo
    {
        public void AddProject(ProjectDTO project);
        public List<ProjectsMembers> GetTeamMemberProjects(string TeamMemberId);

        public void DeleteProject(int ProjectId);
        public List<ProjectsMembers> GetProjectManagerProjects(string ProjectManagerId);

        public Project GetProject(int ProjectId);
        public List<string> GetProjectMembers(int ProjectId);
        public void EditProject(ProjectDTO ProjectDto);
        public void UpdateProjectStatus(int ProjectId);
        public DateTime GetProjectDeadLine(int ProjectId);

        //public List<DateTime> GetDeadLinesProjects(); 



    }
}
