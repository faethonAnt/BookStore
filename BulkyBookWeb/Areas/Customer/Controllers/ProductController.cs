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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _webHostEnvironment = webHostEnvironment;
        
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Upsert(int? id)
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
        if (id == null || id == 0)
        {
            return View(productVM);
        }
        else
        {
            productVM.Product = await _productService.GetProductByIdAsync(id.Value);
            return View(productVM);
        }

    }

    [HttpPost]
    [ActionName("Upsert")]
    [ValidateAntiForgeryToken] // only accepts forms secret code
    public async Task<IActionResult> UpsertPOST(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName =Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                string productPath = Path.Combine("images", "products");
                string finalPath = Path.Combine(wwwRootPath, productPath);

                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }
                
                //save new image
                using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                productVM.Product.ImageUrl = $"/images/products/{fileName}";
            }
            
            await _productService.CreateProductAsync(productVM.Product);
            TempData["Success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        else
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            productVM.CategoryList = categories.Select(c=> new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(productVM);
        }
        
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