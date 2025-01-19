using DoNgocDucTourKitMiTest.Models;
using DoNgocDucTourKitMiTest.Models.DTO;

namespace DoNgocDucTourKitMiTest.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<bool> AddUpdate(Category model);
        Task<bool> Delete(int id);
        Task<Category> GetById(int id);
        Task<List<CategoryGetDTO>> GetAll();
    }
}
