using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ProductDbContext _productDbContext;
    
    public ProductController(ProductDbContext productDbContext)
    {
        _productDbContext = productDbContext;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return _productDbContext.Products;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _productDbContext.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }
        return product;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateProduct(Product product)
    {
        await _productDbContext.Products.AddAsync(product);
        await _productDbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateProduct(Product product)
    {
        _productDbContext.Products.Update(product);
        await _productDbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _productDbContext.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }
        _productDbContext.Products.Remove(product);
        await _productDbContext.SaveChangesAsync();
        return Ok();
    }
}