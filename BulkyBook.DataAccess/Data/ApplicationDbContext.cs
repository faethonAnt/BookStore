using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
namespace BulkyBookWeb.Data;

public class ApplicationDbContext : DbContext
{
    public  ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {}
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Category 1",DisplayOrder = 1},
            new Category { Id = 2, Name = "Category 2",DisplayOrder = 2 },
            new Category { Id = 3, Name = "Category 3",DisplayOrder = 3 }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Product 1",
                Author = "Author 1",
                Description = "Product 1",
                ISBN = "ISBN 1",
                ListPrice = 50,
                Price = 45,
                Price50 = 40,
                Price100 = 35,
                CategoryId = 1
            },
        new Product
        {
            Id = 2,
            Title = "Product 2",
            Author = "Author 2",
            Description = "Product 2",
            ISBN = "ISBN 2",
            ListPrice = 50,
            Price = 45,
            Price50 = 40,
            Price100 = 35,
            CategoryId = 2
        },
            new Product
            {
                Id = 3,
                Title = "Product 3",
                Author = "Author 3",
                Description = "Product 3",
                ISBN = "ISBN 3",
                ListPrice = 50,
                Price = 45,
                Price50 = 40,
                Price100 = 35,
                CategoryId = 3
            });

    }
}