using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace MVCSampleApp.Models
{
    public class Client: Person
    {
        public decimal Balance { get; set; }

        public DateOnly DateOfBirth { get; set; }
        
        public byte[]? Photo { get; set; }

        [NotMapped]
        public IFormFile? PhotoUpload { get; set; }

        public List<Service> Services { get; } = [];

    }


}


