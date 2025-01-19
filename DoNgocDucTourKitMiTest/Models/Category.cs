using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace DoNgocDucTourKitMiTest.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;
        // If I write the following line, it will take a very long time to update database.
        //public List<Product> Products { get; set; }
        //public List<Product> Products { get; set; }
    }
}
