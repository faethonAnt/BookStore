using BulkyBook.Business.IServices;
using BulkyBook.Models;
using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Business.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(bool includeCategory=false)
    {
        if (includeCategory)
        {
            return await _context.Products.Include(u=> u.Category).ToListAsync();
            
        }
        else
        {
            return await _context.Products.ToListAsync();
        }
    }

    public async Task<Product> CreateProductAsync(Product Product)
    {
        _context.Products.Add(Product);
        await _context.SaveChangesAsync();
        return Product;
    }

    public async Task UpdateProductAsync(Product Product)
    {
        _context.Products.Update(Product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var Product = _context.Products.Find(id);
        if (Product == null)
        {
            throw new KeyNotFoundException($"Product {id} not found");
        }
        _context.Products.Remove(Product);
        await _context.SaveChangesAsync();
    }
    
}