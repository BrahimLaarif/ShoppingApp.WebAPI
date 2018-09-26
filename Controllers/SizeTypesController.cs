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
    [Route("api/[controller]")]
    [ApiController]
    public class SizeTypesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SizeTypesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSizeTypes()
        {
            var sizeTypes = await context.SizeTypes.ToListAsync();

            var result = mapper.Map<IEnumerable<SizeTypeResource>>(sizeTypes);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSizeType))]
        public async Task<IActionResult> GetSizeType(int id)
        {
            var sizeType = await context.SizeTypes.FindAsync(id);

            if (sizeType == null)
            {
                return NotFound();
            }

            var result = mapper.Map<SizeTypeResource>(sizeType);

            return Ok(result);
        }
    }
}