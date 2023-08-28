using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        IEnumerable<Customer> GetCustomersByEvent(Guid CustomerId);
        Customer GetCustomersByEvent(Guid CustomerId, Guid Id);
    }
}
