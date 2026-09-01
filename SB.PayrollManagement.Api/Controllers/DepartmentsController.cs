using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Interfaces.Services;

namespace SB.PayrollManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
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

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto)
        {
            var result = await _service.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateDepartmentDto dto)
        {
            if (id <= 0)
            {
                return BadRequest("The ID must be greater than 0");
            }

            var result = await _service.UpdateAsync(id, dto);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("The ID must be greater than 0");
            }

            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
