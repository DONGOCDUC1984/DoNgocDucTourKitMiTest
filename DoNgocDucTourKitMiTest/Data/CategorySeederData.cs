

namespace DoNgocDucTourKitMiTest.Data
{
    public class CategorySeederData
    {
        public static List<Category> GetCategories() {
            var list = new List<Category>();
            for (int i = 0; i < 20; i++)
            {
                list.Add(new Category {Id=i+1,Name = "CategoryName"+ Convert.ToString(i + 1),CreatedDate= DateTime.Parse("2025-01-17") });
            }
            return list;
        }
    }
}
