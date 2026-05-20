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

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        List<Category> categories = await _context.Categories.ToListAsync();

        return categories;
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);
        return category;
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            throw new KeyNotFoundException($"Category {id} not found!");
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsCategoryNameUniqueAsync(string name, int? categoryId = null)
    {
        if (categoryId.HasValue)
        {
            return !await _context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower()
        && c.Id != categoryId);
        }
        else
        {
            return !await _context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
