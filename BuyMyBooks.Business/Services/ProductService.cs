using BuyMyBooks.Business.Services.IServices;
using BuyMyBooks.DataAccess.Data;
using BuyMyBooks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyBooks.Business.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        List<Product> products = await _context.Products.ToListAsync();

        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        Product? product = await _context.Products.FindAsync(id);
        return product;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        Product? product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product {id} not found!");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
