using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Data.Repositories;
using ShoppingApp.WebAPI.Entities.Resources;

namespace ShoppingApp.WebAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IApplicationRepository repository;
        private readonly IMapper mapper;

        public CategoriesController(IApplicationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await repository.GetCategories();

            var result = mapper.Map<IEnumerable<CategoryResource>>(categories);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await repository.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            var result = mapper.Map<CategoryResource>(category);

            return Ok(result);
        }
    }
}