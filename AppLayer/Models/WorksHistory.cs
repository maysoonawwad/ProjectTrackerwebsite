using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLayer.Models
{
    public class WorksHistory
    {

        public DateTime DateTime { get; set; }
        public int WorkId { get; set; }
        public int StatusId { get; set; }
        public string WorkDescription { get; set; }
    }
}
