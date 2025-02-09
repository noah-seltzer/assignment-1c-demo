using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVCSampleApp.Models
{
    [PrimaryKey("ID")]
    public class Service
    {
        public Guid ID { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Service Name - Use roman alphabet only")]
        public string Name { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Rate { get; set; }

        public List<Client>? Clients { get; } = [];
        public List<Employee>? Employees { get; } = [];
    }
}
