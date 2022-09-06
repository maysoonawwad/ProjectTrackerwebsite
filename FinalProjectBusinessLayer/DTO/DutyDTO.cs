using FinalProjectBusinessLayer.entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinalProjectBusinessLayer.DTO
{
    public class DutyDTO
    {
        [Required (ErrorMessage ="Duty Name Require")]
        public string DutyName { get; set; }
        public string DutyDescription { get; set; }
        [Required(ErrorMessage = "Team Member is Required.")]
        public string TeamMemberId { get; set; }
        public TeamMember TeamMember { get; set; }
        public int SprintId { get; set; }
        public int DutyId { get; set; }

        public int StatusId { get; set; }

    }
}
