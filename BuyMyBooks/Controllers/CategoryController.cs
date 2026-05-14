using BuyMyBooks.Data;
using Microsoft.AspNetCore.Mvc;
using BuyMyBooks.Models;

namespace BuyMyBooks.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;
    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<Category> categories = _context.Categories.ToList();
        return View("Index", categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Create")]
    public IActionResult CreatePOST(Category category)
    {
        if (!String.IsNullOrEmpty(category.Name) && _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
        {
            ModelState.AddModelError("", "Category name already exists!");
        }

        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View();
    }
}
