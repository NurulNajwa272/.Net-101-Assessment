using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class EventForCreationDto
    {
        [Required(ErrorMessage = "Event name is required")]
        public string EventName { get; set; }
        public string EventCategory { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public string EventCapacity { get; set; }

    }
}
