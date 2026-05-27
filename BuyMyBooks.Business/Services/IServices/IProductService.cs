using BuyMyBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyBooks.Business.Services.IServices;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(int id);

    Task<List<Product>> GetAllProductsAsync();

    Task<Product> CreateProductAsync(Product product);

    Task UpdateProductAsync(Product product);

    Task DeleteProductAsync(int id);
}
