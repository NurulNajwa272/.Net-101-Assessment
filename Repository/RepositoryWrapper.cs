using Entities.Models;
using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private EventDbContext _dbContext;
        private IEventRepository _event;
        private ICustomerRepository _customer;

        public IEventRepository Event
        {
            get
            {
                if (_event == null)
                {
                    _event = new EventRepository(_dbContext);
                }
                return _event;
            }
        }

        public ICustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_dbContext);
                }
                return _customer;
            }
        }

        public RepositoryWrapper(EventDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
