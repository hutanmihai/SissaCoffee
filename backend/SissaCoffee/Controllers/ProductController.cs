using Microsoft.AspNetCore.Mvc;
using SissaCoffee.Services.ProductService;

namespace SissaCoffee.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
}