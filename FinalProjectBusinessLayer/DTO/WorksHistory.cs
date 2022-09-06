using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBusinessLayer.DTO
{
    public class WorksHistory
    {

       
        public int WorkId { get; set; }
        public string WorkDescription { get; set; }
        public List<DateStatushistory> DateStatus { get; set; }
    }
}
