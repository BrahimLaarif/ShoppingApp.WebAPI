using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Entities.Models;
using ShoppingApp.WebAPI.Entities.Resources;

namespace ShoppingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ColorsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await context.Colors.ToListAsync();

            var result = mapper.Map<IEnumerable<ColorResource>>(colors);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetColor))]
        public async Task<IActionResult> GetColor(int id)
        {
            var color = await context.Colors.FindAsync(id);

            if (color == null)
            {
                return NotFound();
            }

            var result = mapper.Map<ColorResource>(color);

            return Ok(result);
        }
    }
}