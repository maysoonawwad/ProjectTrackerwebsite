using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
    public class Member : IdentityUser
    {
        public string   FirstName { get; set; }
        public string LastName { get; set; }
        public Band band { get; set; }
       
        public int BandId { get; set; }
        public List<ProjectsMembers> projectsMembers { get; set; }
        
        
    }
}
