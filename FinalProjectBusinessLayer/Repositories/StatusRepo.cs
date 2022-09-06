using FinalProjectBusinessLayer.Data;
using FinalProjectBusinessLayer.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProjectBusinessLayer.Repositories
{
    public class StatusRepo : IStatusRepo
    {
        private readonly ApplicationDbContext _AppContext;
        public StatusRepo(ApplicationDbContext AppContext)
        {
            _AppContext = AppContext;
        }

        public List<Status> GetStatuses()
        {
            return _AppContext.Status.ToList();
        }
    }
}
