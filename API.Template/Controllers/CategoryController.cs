using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Interfaces;
using Template.Domain.DTO;
using Template.Domain.Entities;
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
            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
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
            return Ok(_mapper.Map<CategoryDTO>(category));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDTO obj)
        {
            if(ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = obj.Name,
                };
                await _categoryRepository.CreateAsync(category);


                AddCategoryRequestDTO addCategoryRequestDTO = new AddCategoryRequestDTO
                {
                    Name = category.Name,
                };


                return Ok(addCategoryRequestDTO);
            }

            return BadRequest(ModelState);

        }
    }
}
