using DoNgocDucTourKitMiTest.Models;

namespace DoNgocDucTourKitMiTest.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<bool> AddUpdate(Product model);
        Task<bool> Delete(int id);
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
    }
}
