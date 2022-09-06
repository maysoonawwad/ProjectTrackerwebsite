using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBusinessLayer.Repositories
{
   public interface IProjectManagerRepo
    {
        public Task<IdentityResult> RegisterProjectManager(RegisterViewModel model);
        public List<ProjectManager> GetProjectManagers();
        public Task<IdentityResult> UpdateProjectManager(RegisterViewModel ProjectManager);

        public ProjectManager GetProjectManager(string PMId);
        public List<Band> GetBands(); 
    }
}
