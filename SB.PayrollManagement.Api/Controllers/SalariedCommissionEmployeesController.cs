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
    public class SalariedCommissionEmployeesController : ControllerBase
    {
        private readonly ISalariedCommissionEmployeeService _service;

        public SalariedCommissionEmployeesController(ISalariedCommissionEmployeeService service)
        {
            _service = service;
        }

        [HttpGet("{employeeId:int}")]
        public async Task<IActionResult> GetById(int employeeId)
        {
            var result = await _service.GetByIdAsync(employeeId);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] CreateSalariedCommissionEmployeeDto dto)
        {
            var result = await _service.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return CreatedAtAction(nameof(GetById), new { employeeId = result.Data!.EmployeeId }, result);
        }

        [HttpPut("{employeeId:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(int employeeId, [FromBody] UpdateSalariedCommissionEmployeeDto dto)
        {
            var result = await _service.UpdateAsync(employeeId, dto);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
