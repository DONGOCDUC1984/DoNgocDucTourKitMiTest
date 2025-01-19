
namespace DoNgocDucTourKitMiTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasIndex(x => x.Name)
                        .IsUnique();
            modelBuilder.Entity<Product>()
                       .HasIndex(x => x.Name)
                       .IsUnique();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                CategorySeederData.GetCategories()
            );
            modelBuilder.Entity<Product>().HasData(
                ProductSeederData.GetProducts()
            );
        }

    }
}
