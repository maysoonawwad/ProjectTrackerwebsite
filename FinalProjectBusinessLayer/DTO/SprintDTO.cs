using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinalProjectBusinessLayer.DTO
{
    public class SprintDTO
    {
        public int SprintId { get; set; }
        [Required (ErrorMessage ="Start Date is required")]
        public DateTime StartDate { get; set; }
        [Required (ErrorMessage ="End Date is required")]
        public DateTime EndDate { get; set; }
        public string TeamLeaderId { get; set; }
        public int StatusId { get; set; }
        public int ProjectId { get; set; }


    }
}
