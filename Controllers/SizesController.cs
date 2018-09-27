using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Entities.Resources;

namespace ShoppingApp.WebAPI.Controllers
{
    [Route("api/sizes")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SizesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSizes()
        {
            var sizes = await context.Sizes.ToListAsync();

            var result = mapper.Map<IEnumerable<SizeResource>>(sizes);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSize))]
        public async Task<IActionResult> GetSize(int id)
        {
            var size = await context.Sizes.FindAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            var result = mapper.Map<SizeResource>(size);

            return Ok(result);
        }
    }
}