using BuyMyBooks.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using BuyMyBooks.Models;
using BuyMyBooks.Business.Services.IServices;

namespace BuyMyBooks.Areas.Customer.Controllers;

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
        List<Product> products = await _productService.GetAllProductsAsync();
        return View("Index", products);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Create")]
    public async Task<IActionResult> CreatePOST(Product product)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateProductAsync(product);
            TempData["success"] = "Product created successfully";
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

        Product? product = await _productService.GetProductByIdAsync(id.Value);
        if(product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Update")]
    public async Task<IActionResult> UpdatePOST(Product product)
    {
        if (ModelState.IsValid)
        {
            await _productService.UpdateProductAsync(product);            
            TempData["success"] = "Product updated successfully";
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

        Product? product = await _productService.GetProductByIdAsync(id.Value);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(int id)
    {
        await _productService.DeleteProductAsync(id);
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
    }
}
