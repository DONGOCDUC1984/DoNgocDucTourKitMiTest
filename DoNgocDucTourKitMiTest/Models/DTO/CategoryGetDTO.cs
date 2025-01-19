
namespace DoNgocDucTourKitMiTest.Models.DTO
{
    public class CategoryGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;
        public int NumberProducts { get; set; }
    }
}
