using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSampleApp.Models
{
    public class Employee : Person
    {
        public decimal Salary { get; set; }

        public Service? Service { get; set; } = null!;

        [ForeignKey(nameof(Service))]
        public Guid? ServiceID { get; set; }
    }
}
