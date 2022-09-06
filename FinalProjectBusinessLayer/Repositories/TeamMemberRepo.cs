using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBusinessLayer.Repositories
{
   public class TeamMemberRepo : ITeamMemberRepo
    {
        private readonly ApplicationDbContext _AppContext;
       
        private readonly UserManager<Member> _userManager;


        public TeamMemberRepo(UserManager<Member> userManager , ApplicationDbContext AppContext)
        {
            _userManager = userManager;
            _AppContext = AppContext;


        }

        public List<TeamMember> GetTeamMembers()
        {
            return _AppContext.TeamMembers.ToList();
        }

        public List<TeamMember> GetTeamMembersProject(int projectId)
        {
            List<TeamMember> projectmembers = new List<TeamMember>();
            //var result = _AppContext.projectsMembers.Include(x => x.Member).Include(x => x.Project).Where(x => x.ProjectId == projectId).ToList();
           var res = _AppContext.projectsMembers.Join(_AppContext.Users, p => p.Id, u => u.Id, (p, u) => new { p.Id, u.FirstName, u.LastName, u.BandId, p.ProjectId }).Join(_AppContext.Projects.Where(x => x.ProjectId == projectId) , u=> u.ProjectId , p => p.ProjectId , (u ,p) => new {u.LastName , u.FirstName , u.Id , u.ProjectId } ).Join(_AppContext.UserRoles.Where(x => x.RoleId == "4"), p => p.Id, r => r.UserId, (p, r) => new { p.ProjectId , p.FirstName , p.LastName , p.Id}).ToList();
           
            foreach(var tm in res)
            {
                projectmembers.Add(new TeamMember()
                {
                    FirstName = tm.FirstName,
                    LastName = tm.LastName,
                    Id = tm.Id
                });
            }
            return projectmembers;


        }

        public async Task<IdentityResult> RegisterTeamMember(RegisterViewModel model)
        {
            var user = new TeamMember
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BandId = model.BandId,

                EmailConfirmed = true

            };

            var result = await _userManager.CreateAsync(user, model.Password);
            var userFromDb = _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(await userFromDb, "teammember");

            return result;

        }
    }
}
