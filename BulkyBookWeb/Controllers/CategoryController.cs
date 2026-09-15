using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;
    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var categories = _context.Categories.ToList(); // EF alternative to SELECT
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
        if (!String.IsNullOrEmpty(category.Name) && _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
        {
            ModelState.AddModelError("", "Category already exists");
        }
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View();
        
    }
}