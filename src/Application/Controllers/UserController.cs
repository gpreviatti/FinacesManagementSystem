using System;
using System.Threading.Tasks;
using Domain.Dtos.User;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("users")]
    public class UserController : DefaultController
    {
        IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.FindAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateAsync(userCreateDto);
            if (result == null)
            {
                return BadRequest("Fail to create User");   
            }

            return Created("/users", result);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateAsync(userUpdateDto);

            if (result == null)
            {
                return BadRequest("Fail to update User");
            }

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.DeleteAsync(Id);
            if (result == false)
            {
                return BadRequest("Fail to delete User");
            }

            return Ok("User deleted with success");
        }
    }
}
