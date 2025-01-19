
namespace DoNgocDucTourKitMiTest.Data
{
    public class ProductSeederData
    {
        // Get random number from min to max
        private static int GetRandomNumer(int min, int max)
        {
            Random rd = new Random();
            int rand_num = rd.Next(min, max);
            return rand_num;
        }
        public static List<Product> GetProducts()
        {
            var list = new List<Product>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(new Product {Id=i+1, Name = "ProductName" + Convert.ToString(i + 1),
                    Price=GetRandomNumer(1,11) ,CategoryId= GetRandomNumer(1, 21) ,CreatedDate = DateTime.Parse("2025-01-17") });
            }
            return list;
        }
    }
}
