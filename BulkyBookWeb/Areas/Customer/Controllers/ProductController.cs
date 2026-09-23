using BulkyBook.Business.IServices;
using BulkyBookWeb.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers;

[Area("Customer")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
        
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Upsert()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        ProductVM productVM = new()
        {
            CategoryList = categories.Select(c=> new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }),
            Product = new Product()
        };
        return View(productVM);
    }

    [HttpPost]
    [ActionName("Create")]
    [ValidateAntiForgeryToken] // only accepts forms secret code
    public async Task<IActionResult> Upsert(Product product)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateProductAsync(product);
            TempData["Success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        return View();
        
    }

    

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var product = _productService.GetProductByIdAsync(id.Value).Result;
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(int id)
    {
        await _productService.DeleteProductAsync(id);
        TempData["Success"] = "Product deleted successfully";
        return RedirectToAction("Index");
        
    }
    
    #region API CALLS

    //json 
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync(true);// EF alternative to SELECT
        return Json(new {data = products});
    }

    #endregion
}