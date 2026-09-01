using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Interfaces.Services;

namespace SB.PayrollManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _usersService.GetAllAsync();
            if (!result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("The ID must be greater than 0");
            }

            var result = await _usersService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var result = await _usersService.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }
    }
}
