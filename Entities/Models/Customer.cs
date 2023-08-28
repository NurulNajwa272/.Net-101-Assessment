using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("customer")]
    public class Customer
    {
        [Column("CustomerId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string CustomerEmail { get; set; }

        [ForeignKey(nameof(Event))]
        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public bool IsEmptyObject()
        {
            throw new NotImplementedException();
        }
    }
}
