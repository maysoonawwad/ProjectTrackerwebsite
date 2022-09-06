using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.entities
{
   public class Band
    {
        public int BandId { get; set; }
        public string BandTitle { get; set; }
        public List<Member> Members { get; set; }
    }
}
