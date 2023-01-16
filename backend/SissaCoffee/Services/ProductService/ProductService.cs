using AutoMapper;
using SissaCoffee.Models.DTOs.Product;
using SissaCoffee.Repositories.ProductRepository;

namespace SissaCoffee.Services.ProductService;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDTO>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return _mapper.Map<List<ProductDTO>>(products);
    }
}