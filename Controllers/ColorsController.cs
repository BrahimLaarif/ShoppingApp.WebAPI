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
    [Route("api/colors")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IApplicationRepository repository;
        private readonly IMapper mapper;

        public ColorsController(IApplicationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await repository.GetColors();

            var result = mapper.Map<IEnumerable<ColorResource>>(colors);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetColor))]
        public async Task<IActionResult> GetColor(int id)
        {
            var color = await repository.GetColor(id);

            if (color == null)
            {
                return NotFound();
            }

            var result = mapper.Map<ColorResource>(color);

            return Ok(result);
        }
    }
}