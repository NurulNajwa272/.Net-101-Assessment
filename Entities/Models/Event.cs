using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("event")]
    public class Event
    {
        [Column("EventId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        public string EventName { get; set; }
        public string EventCategory { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public string EventCapacity { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
