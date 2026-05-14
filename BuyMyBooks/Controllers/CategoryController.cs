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
}
