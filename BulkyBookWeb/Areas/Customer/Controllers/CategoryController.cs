using BulkyBook.Business.IServices;
using BulkyBookWeb.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

[Area("Customer")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();// EF alternative to SELECT
        return View("Index", categories);
    }

    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Create")]
    [ValidateAntiForgeryToken] // only accepts forms secret code
    public async Task<IActionResult> CreatePOST(Category category)
    {
        if (!String.IsNullOrEmpty(category.Name) &&
            !await _categoryService.IsCategoryNameUniqueAsync(category.Name, category.Id))
        {
            ModelState.AddModelError("", "Category already exists");
        }
        if (ModelState.IsValid)
        {
            await _categoryService.CreateCategoryAsync(category);
            TempData["Success"] = "Category created successfully";
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
        var category = _categoryService.GetCategoryByIdAsync(id.Value).Result;
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ActionName("Update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePOST(Category category)
    {
        if (!String.IsNullOrEmpty(category.Name) && 
             !await _categoryService.IsCategoryNameUniqueAsync(category.Name, category.Id))
        {
            ModelState.AddModelError("", "Category already exists");
        }
        if (ModelState.IsValid)
        {
            await _categoryService.UpdateCategoryAsync(category);
            TempData["Success"] = "Category updated successfully";
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
        var category = _categoryService.GetCategoryByIdAsync(id.Value).Result;
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        TempData["Success"] = "Category deleted successfully";
        return RedirectToAction("Index");
        
    }

    #region API CALLS

    //json 
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();// EF alternative to SELECT
        return Json(new {data = categories});
    }

    #endregion
}