using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
   public class WorkAttachment
    {
        public int WorkAttachmentId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Byte[] FileData { get; set; }
        public Work Work { get; set; }
        public int WorkId  { get; set; }
    }
}
