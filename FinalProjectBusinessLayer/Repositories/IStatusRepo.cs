using FinalProjectBusinessLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.Repositories
{
   public interface IStatusRepo
    {

        public List<Status> GetStatuses();
    }
}
