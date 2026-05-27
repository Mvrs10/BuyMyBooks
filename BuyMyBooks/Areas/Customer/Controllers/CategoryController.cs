using BuyMyBooks.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using BuyMyBooks.Models;
using BuyMyBooks.Business.Services.IServices;

namespace BuyMyBooks.Areas.Customer.Controllers;

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
        List<Category> categories = await _categoryService.GetAllCategoriesAsync();
        return View("Index", categories);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Create")]
    public async Task<IActionResult> CreatePOST(Category category)
    {
        if (!string.IsNullOrEmpty(category.Name) && !await _categoryService.IsCategoryNameUniqueAsync(category.Name, category.Id))
        {
            ModelState.AddModelError("", "Category name already exists!");
        }

        if (ModelState.IsValid)
        {
            await _categoryService.CreateCategoryAsync(category);
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public async Task<IActionResult> Update(int? id)
    {
        if(id == null || id == 0)
        {
            return NotFound();
        }

        Category? category = await _categoryService.GetCategoryByIdAsync(id.Value);
        if(category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Update")]
    public async Task<IActionResult> UpdatePOST(Category category)
    {
        if (!string.IsNullOrEmpty(category.Name) && !await _categoryService.IsCategoryNameUniqueAsync(category.Name, category.Id))
        {
            ModelState.AddModelError("", "Category name already exists!");
        }

        if (ModelState.IsValid)
        {
            await _categoryService.UpdateCategoryAsync(category);            
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? category = await _categoryService.GetCategoryByIdAsync(id.Value);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
