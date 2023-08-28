using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string CustomerEmail { get; set; }

        //public IEnumerable<EventDto> Events { get; set; }

    }
}
