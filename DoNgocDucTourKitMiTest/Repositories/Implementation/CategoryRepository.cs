
namespace DoNgocDucTourKitMiTest.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _ctx;
        public CategoryRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        // Add or Update
        public async Task<bool> AddUpdate(Category category)
        {
            try
            {
                // Add
                if (category.Id == 0)
                    await _ctx.Categories.AddAsync(category);
                // Update
                else
                    _ctx.Categories.Update(category);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await GetById(id);
                if (data == null)
                { return false; }

                _ctx.Categories.Remove(data);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CategoryGetDTO>> GetAll()
        {
            var categoryGetDTOList = new List<CategoryGetDTO>();
            var categoriesList = await _ctx.Categories.ToListAsync();
            for (int i = 0; i < categoriesList.Count; i++)
            {
                var productList= await _ctx.Products
                    .Where(x=>x.CategoryId== categoriesList[i].Id)
                    .ToListAsync();
                var newCategoryGetDTO = new CategoryGetDTO() 
                {
                    Id= categoriesList[i].Id,
                    Name= categoriesList[i].Name,
                    CreatedDate= categoriesList[i].CreatedDate,
                    NumberProducts= productList.Count,
                };
                categoryGetDTOList.Add(newCategoryGetDTO);  
            }
            return categoryGetDTOList;
        }

        public async Task<Category> GetById(int id)
        {
            return await _ctx.Categories.FindAsync(id);
        }

    }
}
