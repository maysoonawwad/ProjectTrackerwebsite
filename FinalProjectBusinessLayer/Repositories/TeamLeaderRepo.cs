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
    public class TeamLeaderRepo : ITeamLeaderRepo
    {
        private readonly UserManager<Member> _userManager;

        private readonly ApplicationDbContext _AppContext;

        
        public TeamLeaderRepo(UserManager<Member> userManager , ApplicationDbContext AppContext)
        {
            _userManager = userManager;
            _AppContext = AppContext;


        }

        public async Task<IdentityResult> RegisterTeamLeader(RegisterViewModel model)
        {
            var user = new TeamLeader
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
            await _userManager.AddToRoleAsync(await userFromDb, "teamleader");

            return result;

        }


        public List<ProjectsMembers> GetTeamLeaderProjects(string TeamLeaderId)
        {
            return _AppContext.projectsMembers.Include(x => x.Project).Where(x => x.Id == TeamLeaderId).ToList();
        }

        public List<TeamLeader> GetTeamLeaders()
        {
            return _AppContext.TeamLeaders.ToList();
        }
    }
}
