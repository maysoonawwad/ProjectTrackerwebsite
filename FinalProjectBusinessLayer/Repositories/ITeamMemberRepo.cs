using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBusinessLayer.Repositories
{
    public interface ITeamMemberRepo
    {
        public Task<IdentityResult> RegisterTeamMember(RegisterViewModel model);

        public List<TeamMember> GetTeamMembers();
        public List<TeamMember> GetTeamMembersProject(int projectId );
    }
}
