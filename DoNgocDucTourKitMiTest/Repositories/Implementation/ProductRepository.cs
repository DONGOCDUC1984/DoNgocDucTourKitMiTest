using DoNgocDucTourKitMiTest.Redis;

namespace DoNgocDucTourKitMiTest.Repositories.Implementation
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IRedisCacheService _redisCacheService;
        public ProductRepository(AppDbContext ctx, IRedisCacheService redisCacheService)
        {
            _ctx = ctx;
            _redisCacheService = redisCacheService;
        }
        public async Task<bool> AddUpdate(Product product)
        {
            try
            {
                // Add
                if (product.Id == 0)
                    await _ctx.Products.AddAsync(product);
                // Update
                else
                    _ctx.Products.Update(product);
                _redisCacheService.RemoveData("Product");
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

                _ctx.Products.Remove(data);
                _redisCacheService.RemoveData("Product");
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Product>> GetAll()
        {
            var expirationTime = DateTimeOffset.Now.AddDays(2);
            var cacheData = _redisCacheService.GetData<List<Product>>("Product");
            if (cacheData != null)
            {
                return cacheData;
            }
            var data = await _ctx.Products
                .Include(x => x.Category)
                .ToListAsync();
            _redisCacheService.SetData<List<Product>>("Product", data, expirationTime);
            cacheData = data;
            return cacheData;
        }

        public async Task<Product> GetById(int id)
        {
            var list = await GetAll();
            var data = list.FirstOrDefault(x => x.Id == id);
            return data;
        }
    }
}
