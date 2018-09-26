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
    public class MaterialsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MaterialsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaterials()
        {
            var materials = await context.Materials.ToListAsync();

            var result = mapper.Map<IEnumerable<MaterialResource>>(materials);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetMaterial))]
        public async Task<IActionResult> GetMaterial(int id)
        {
            var material = await context.Materials.FindAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            var result = mapper.Map<MaterialResource>(material);

            return Ok(result);
        }
    }
}