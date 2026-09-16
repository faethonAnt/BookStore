using BulkyBook.Models;

namespace BulkyBook.Business.IServices;

public interface ICategoryService
{
    Task<Category?> GetCategoryByIdAsync(int id);
    
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    
    Task<Category> CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    
    Task DeleteCategoryAsync(int id);
    
    Task<bool> IsCategoryNameUniqueAsync(String Name,int? categoryId=null);
}