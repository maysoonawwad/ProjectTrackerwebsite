using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.DTO;
using FinalProjectBusinessLayer.entities;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBusinessLayer.Repositories
{
   public class ProjectManagerRepo : IProjectManagerRepo
    {
        private readonly UserManager<Member> _userManager;

        private readonly ApplicationDbContext _AppContext;

        public ProjectManagerRepo(UserManager<Member> userManager, ApplicationDbContext appContext)
        {
            _userManager = userManager;
            _AppContext = appContext;


        }

        public List<Band> GetBands()
        {
            return _AppContext.Bands.ToList();
        }

        public ProjectManager GetProjectManager(string PMId)
        {
            return _AppContext.ProjectManagers.SingleOrDefault(x => x.Id == PMId);
        }

        public List<ProjectManager> GetProjectManagers()
        {
            return _AppContext.ProjectManagers.ToList();
        }
       

        public async Task<IdentityResult> RegisterProjectManager(RegisterViewModel model)
        {
            
          
              var user = new ProjectManager
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
           await _userManager.AddToRoleAsync(await userFromDb, "projectmanager");

            return result;


        }
        public async Task<IdentityResult> UpdateProjectManager(RegisterViewModel model)
        {

            var user = _AppContext.ProjectManagers.SingleOrDefault(x => x.Id == model.Id);
              

                if(user != null)
            {
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BandId = model.BandId;
            }
            var result = await _userManager.UpdateAsync(user);
            return result;


        }

    }
}
