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
    [Route("/api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UsersController(IApplicationRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await repository.GetUsers();

            var result = mapper.Map<IEnumerable<UserResource>>(users);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetUser))]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await repository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = mapper.Map<UserResource>(user);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserResource payload)
        {
            var user = mapper.Map<User>(payload);

            repository.AddUser(user, payload.Password);
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<UserResource>(user);

            return CreatedAtRoute(nameof(GetUser), new { id = user.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateUserResource payload)
        {
            var user = await repository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            mapper.Map<UpdateUserResource, User>(payload, user);

            await unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await repository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            repository.RemoveUser(user);
            await unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}