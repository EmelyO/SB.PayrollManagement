using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.PayrollManagement.Application.Interfaces.Services;

namespace SB.PayrollManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync();
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

            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
