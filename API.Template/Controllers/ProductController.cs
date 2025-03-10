using API.Template.CustomActionFilters;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Interfaces;
using Template.Domain.DTO;
using Template.Domain.Entities;

namespace API.Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        //Get All Walk
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            //Get Data from Database - domain models
            var walk = await _productRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //return DTO
            return Ok(_mapper.Map<List<ProductDto>>(walk));
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id) 
        {
            Product? product = await _productRepository.GetByIdAsync(Id);
            if (product is null) 
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(product));
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddProductRequestDto addProductRequestDto) 
        {
            //Convert DTO to Domain Model
            var productDomain = _mapper.Map<Product>(addProductRequestDto);
            //Use Domain Model to create a product
            await _productRepository.CreateAsync(productDomain);
            //Map Domain model back to Dto
            var productDto = _mapper.Map<AddProductRequestDto>(productDomain);

            return Ok(productDto);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductDto updateProductDto)
        {
            //Map Dto to Domain
            var productDomain = _mapper.Map<Product>(updateProductDto);
            //Check if walk exists
            productDomain = await _productRepository.UpdateAsync(id, productDomain);
            if (productDomain is null)
            {
                return NotFound();
            }
            //Convert Back to Dto
            var productDto = _mapper.Map<ProductDto>(productDomain);
            //return Dto
            return Ok(productDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Check if Walk exists
            var productDomain = await _productRepository.DeleteAsync(id);
            if (productDomain is null)
            {
                return NotFound();
            }
            //return deleted region back optional
            return Ok(_mapper.Map<ProductDto>(productDomain));
        }
    }
}
