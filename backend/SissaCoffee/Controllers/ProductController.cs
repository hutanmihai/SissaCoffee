using Microsoft.AspNetCore.Mvc;
using SissaCoffee.Models.DTOs.Product;
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
    
    [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, [FromBody] ProductUpdateDTO dto)
        {
            try
            {
                await _productService.UpdateProductAsync(id, dto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct([FromBody] ProductCreateDTO dto)
        {
            try
            {
                return await _productService.CreateProductAsync(dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return Accepted();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
}