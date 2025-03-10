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
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
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
