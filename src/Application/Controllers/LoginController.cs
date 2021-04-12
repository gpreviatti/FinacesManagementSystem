using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("login")]
    public class LoginController : DefaultController
    {
        ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.Login(loginDto);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
