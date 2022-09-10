using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProjectBusinessLayer.Repositories
{
    public class ProjectRepo : IProjectRepo
    {
        private readonly ApplicationDbContext _AppContext;
        public ProjectRepo(ApplicationDbContext AppContext)
        {
            _AppContext = AppContext;
        }

        public void AddProject(ProjectDTO projectdto)
        {
             var project = new Project();
            project.ProjectId = projectdto.ProjectId;
            project.DeadLine = projectdto.DeadLine;
            project.ProjectTitle = projectdto.ProjectTitle;
            project.ProjectDescription = projectdto.ProjectDescription;
            project.StatusId = projectdto.StatusId;
            _AppContext.Projects.Add(project);
            _AppContext.SaveChanges();

            foreach (var memberIds in projectdto.Ids)
            {
                _AppContext.projectsMembers.Add(new ProjectsMembers()
                {
                    Id = memberIds,
                    ProjectId = project.ProjectId
                });
            }
          
            _AppContext.SaveChanges();
             
        }

        public void DeleteProject(int ProjectId)
        {
            var projectmembers = _AppContext.projectsMembers.Where(x => x.ProjectId == ProjectId).ToList();
            
                _AppContext.projectsMembers.RemoveRange(projectmembers);
            
            _AppContext.SaveChanges();

            var sprints = _AppContext.Sprints.Where(x => x.ProjectId == ProjectId).ToList();
            foreach (var result in sprints)
            {
                var duties = _AppContext.Duties.Where(x => x.SprintId == result.SprintId).ToList();
                

                foreach (var duty in duties)
                {
                    var Works = _AppContext.Works.Where(x => x.DutyId == duty.DutyId).ToList();
                    foreach (var work in Works)
                    {
                        var attach = _AppContext.WorkAttachments.Where(x => x.WorkId == work.WorkId).ToList();
                        _AppContext.WorkAttachments.RemoveRange(attach);
                        _AppContext.SaveChanges();

                        var history = _AppContext.WorkHistories.Where(x => x.WorkId == work.WorkId).ToList();
                        _AppContext.WorkHistories.RemoveRange(history);
                        _AppContext.SaveChanges();
                    }
                    _AppContext.Works.RemoveRange(Works);
                    _AppContext.SaveChanges();


                }
                _AppContext.Duties.RemoveRange(duties);
                _AppContext.SaveChanges();

            }
            _AppContext.Sprints.RemoveRange(sprints);
            _AppContext.SaveChanges();

            var project = _AppContext.Projects.SingleOrDefault(x => x.ProjectId == ProjectId);
            _AppContext.Projects.Remove(project);
            _AppContext.SaveChanges();

        }

        public List<ProjectsMembers> GetProjectManagerProjects(string ProjectManagerId)
        {
            
                var projects = _AppContext.projectsMembers.Include(x => x.Project).Where(x => x.Id == ProjectManagerId).ToList();
                
            return projects;
        }
        //to edit project
        public Project GetProject(int ProjectId)
        {
            return _AppContext.Projects.SingleOrDefault(x => x.ProjectId == ProjectId);
        }
        public List<string> GetProjectMembers(int ProjectId)
        {
            var projectsMembers =  _AppContext.projectsMembers.Where(x => x.ProjectId == ProjectId).ToList();
            List<string> MembersId = new List<string>();

            foreach(var PM in projectsMembers)
            {
                if(PM.ProjectId == ProjectId)
                {
                    MembersId.Add(PM.Id);
                }
            }
            return MembersId;
        }

        public List<ProjectsMembers> GetTeamMemberProjects(string TeamMemberId)
        {
            return _AppContext.projectsMembers.Include(x => x.Project).Where(x => x.Id == TeamMemberId).ToList();

        }

        public void EditProject(ProjectDTO projectdto)
        {
            var project = new Project();
            project.ProjectId = projectdto.ProjectId;
            project.DeadLine = projectdto.DeadLine;
            project.ProjectTitle = projectdto.ProjectTitle;
            project.ProjectDescription = projectdto.ProjectDescription;
            project.StatusId = projectdto.StatusId;
            _AppContext.Projects.Update(project);
            _AppContext.SaveChanges();

            var members = _AppContext.projectsMembers.Where(x => x.ProjectId == project.ProjectId);
            _AppContext.projectsMembers.RemoveRange(members);
            foreach (var memberIds in projectdto.Ids)
            {
                _AppContext.projectsMembers.Add(new ProjectsMembers()
                {
                    Id = memberIds,
                    ProjectId = project.ProjectId
                });
            }

            _AppContext.SaveChanges();

        }

        public void UpdateProjectStatus(int ProjectId)
        {
            var project = _AppContext.Projects.SingleOrDefault(x => x.ProjectId == ProjectId);
            project.StatusId = 2;
            _AppContext.SaveChanges();
        }

        public DateTime GetProjectDeadLine(int ProjectId)
        {
            var result = _AppContext.Projects.SingleOrDefault(x => x.ProjectId == ProjectId);
            return result.DeadLine;

        }



    }
    }
