using Entities.Models;
using Events;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(EventDbContext eventDbContext)
            : base(eventDbContext)
        {
        }
        public IEnumerable<Customer> GetCustomersByEvent(Guid CustomerId)
        {
            var customers = FindByCondition(cs => cs.EventId.Equals(CustomerId));
            return customers;
        }

        public Customer GetCustomersByEvent(Guid CustomerId, Guid Id)
        {
            return FindByCondition(cs => cs.EventId.Equals(CustomerId) && cs.Id.Equals(Id)).SingleOrDefault();
        }



    }
}
