using FinalProjectBusinessLayer.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinalProjectBusinessLayer.DTO
{
    public class WorkDTO
    {
        public int WorkId { get; set; }
        [Required (ErrorMessage ="Work description is required")]
        public string WorkDescription { get; set; }
        public int DutyId { get; set; }
        public int StatusId { get; set; }
        public int WorkAttachmentId { get; set; }
        public string RejectNote { get; set; }
        public Byte[] FileData { get; set; }

    }
}
