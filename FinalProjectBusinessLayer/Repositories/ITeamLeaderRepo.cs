using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBusinessLayer.Repositories
{
    public interface ITeamLeaderRepo
    {
        public Task<IdentityResult> RegisterTeamLeader(RegisterViewModel model);
        public List<ProjectsMembers> GetTeamLeaderProjects(string TeamLeaderId);
        public List<TeamLeader> GetTeamLeaders();

    }
}
