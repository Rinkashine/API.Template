using API.Template.CustomActionFilters;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Interfaces;
using Template.Domain.DTO;
using Template.Domain.Entities;
using Template.Infastructure.Migrations;
using Template.Infastructure.Repository;

namespace API.Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database
            List<Category> categories = await _categoryRepository.GetAllAsync();
            //Return DTO
            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            //Return Dto
            return Ok(_mapper.Map<CategoryDto>(category));
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto addCategoryRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var categoryDomain = _mapper.Map<Category>(addCategoryRequestDTO);
            //Use Domain Model to Create Region
            await _categoryRepository.CreateAsync(categoryDomain);
            //Map Domain model back to DTO
            var walkDto = _mapper.Map<AddCategoryRequestDto>(categoryDomain);

            return Ok(walkDto);
        }
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var categoryDomain = _mapper.Map<Category>(updateCategoryRequestDto);
            //Use Domain Model to Create Region
            await _categoryRepository.UpdateAsync(categoryDomain, id);
            //Map Domain model back to Dto
            var categoryDto = _mapper.Map<UpdateCategoryRequestDto>(categoryDomain);

            return Ok(categoryDto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Delete Category
            var categoryDomain = await _categoryRepository.DeleteAsync(id);
            if (categoryDomain is null)
            {
                return NotFound();
            }
            //return deleted region back optional
            return Ok(_mapper.Map<CategoryDto>(categoryDomain));
        }
        
    }
}
