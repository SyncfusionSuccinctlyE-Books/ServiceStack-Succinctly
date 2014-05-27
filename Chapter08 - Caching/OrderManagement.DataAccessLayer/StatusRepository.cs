using System;
using System.Collections.Generic;
using System.Linq;
using OrderManagement.Core.Domain;

namespace OrderManagement.DataAccessLayer
{
    public interface IStatusRepository
    {
        Status GetById(int id); //returns an order given the order id        
    }

    /// <summary>
    /// In-memory orders repository, that fakes the database
    /// </summary>
    public class StatusRepository : IStatusRepository
    {
        private static readonly List<Status> Statuses = new List<Status>();       

        public StatusRepository()
        {
            Init();
        }

        public void Init()
        {
            if (!Statuses.Any())
            {
                //Create three orders.
                Statuses.Add(new Status {Id = 1, Name = "Active"});
                Statuses.Add(new Status {Id = 2, Name = "Inactive"});
            }
        }
      
        public Status GetById(int id)
        {
            return Statuses.FirstOrDefault(x => x.Id == id);
        }      
    }
}