using BulkyBook.Business.IServices;
using BulkyBookWeb.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

[Area("Customer")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Create")]
    [ValidateAntiForgeryToken] // only accepts forms secret code
    public async Task<IActionResult> CreatePOST(Product product)
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
    public IActionResult Update(int? id)
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
    [ActionName("Update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePOST(Product product)
    {
        if (ModelState.IsValid)
        {
            await _productService.UpdateProductAsync(product);
            TempData["Success"] = "Product updated successfully";
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