using Microsoft.EntityFrameworkCore;

namespace MVCSampleApp.Models
{
    [PrimaryKey("ID")]
    public class Person
    {
        public Guid ID { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

    }
}
