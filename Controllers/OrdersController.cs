using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Data.Repositories;

namespace ShoppingApp.WebAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IApplicationRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public OrdersController(IApplicationRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await repository.GetOrders();

            return Ok(orders);
        }

        [HttpGet("{id}", Name = nameof(GetOrder))]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await repository.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}