using BulkyBook.Models;

namespace BulkyBook.Business.IServices;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(int id);
    
    Task<IEnumerable<Product>> GetAllProductsAsync(bool includeCategory=false);
    
    Task<Product> CreateProductAsync(Product Product);
    Task UpdateProductAsync(Product Product);
    
    Task DeleteProductAsync(int id);

}