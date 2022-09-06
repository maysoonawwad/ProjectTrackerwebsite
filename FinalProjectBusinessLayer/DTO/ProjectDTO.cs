using FinalProjectBusinessLayer.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinalProjectBusinessLayer.DTO
{
   public  class ProjectDTO
    {
        public int ProjectId { get; set; }
        [Required (ErrorMessage ="Project title is required")]
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        [Required]
        public DateTime DeadLine { get; set; }
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Team leader is required.")]

        public List<string> Ids { get; set; }
        [Required(ErrorMessage = "Team leader is required.")]

        public string TeamLeaderId { get; set; }

        public TeamLeader TeamLeader { get; set; }


    }
}
