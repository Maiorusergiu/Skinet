using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepository,
    IGenericRepository<ProductBrand> ProductBrandRepository,
    IGenericRepository<ProductType> ProductTypeRepository, IMapper mapper)
    {
            _productRepository = ProductRepository;
            _productBrandRepository = ProductBrandRepository;
            _productTypeRepository = ProductTypeRepository;
            _mapper = mapper;
        }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await _productRepository.ListAsync(spec);
        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }
    //specification pattern starts here:
    //The app hits the id in API
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        // He reads the new instance of the class with the parameter and we go to ProductsWithTypesAndBrandsSpecification(id)
        var product = await _productRepository.GetEntityWithSpec(spec);
        return _mapper.Map<Product, ProductToReturnDto>(product);
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyCollection<ProductBrand>>> GetProductBrands()
    {
        return Ok( await _productBrandRepository.ListAllAsync());
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyCollection<ProductType>>> GetProductTypes()
    {
        return Ok( await _productTypeRepository.ListAllAsync());
    }

}
}
