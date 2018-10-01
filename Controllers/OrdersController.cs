using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Data.Repositories;
using ShoppingApp.WebAPI.Entities.Models;
using ShoppingApp.WebAPI.Entities.Resources;

namespace ShoppingApp.WebAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IApplicationRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrdersController(IApplicationRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await repository.GetOrders();

            var result = mapper.Map<IEnumerable<OrderResource>>(orders);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetOrder))]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await repository.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            var result = mapper.Map<OrderResource>(order);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddOrderResource payload)
        {
            var user = await repository.GetUser(payload.UserId);

            if (user == null)
            {
                ModelState.AddModelError("UserId", "Invalid UserId");
                return BadRequest(ModelState);
            }

            if (!Enum.IsDefined(typeof(ShippingMethods), payload.ShippingMethod))
            {
                ModelState.AddModelError("ShippingMethod", "Invalid ShippingMethod");
                return BadRequest(ModelState);
            }

            foreach(var item in payload.Items)
            {
                if (await repository.GetModel(item.ModelId) == null)
                {
                    ModelState.AddModelError("ModelId", "Invalid ModelId");
                    return BadRequest(ModelState);
                }

                if (await repository.GetSize(item.SizeId) == null)
                {
                    ModelState.AddModelError("SizeId", "Invalid SizeId");
                    return BadRequest(ModelState);
                }
            }

            var order = mapper.Map<Order>(payload);

            foreach(var item in order.Items)
            {
                var model = await repository.GetModel(item.ModelId);

                item.Amount = model.Price;
            }

            repository.AddOrder(order);
            await unitOfWork.CompleteAsync();

            var result = await repository.GetOrder(order.Id);

            return CreatedAtRoute(nameof(GetOrder), new { id = order.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await repository.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            repository.RemoveOrder(order);
            await unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}