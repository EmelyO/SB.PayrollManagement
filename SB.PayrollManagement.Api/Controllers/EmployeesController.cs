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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPayrollService _payrollService;

        public EmployeesController(IEmployeeService employeeService, IPayrollService payrollService)
        {
            _employeeService = employeeService;
            _payrollService = payrollService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetAllAsync();
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

            var result = await _employeeService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("{id:int}/pay")]
        public async Task<IActionResult> GetPay(int id)
        {
            if (id <= 0)
            {
                return BadRequest("The ID must be greater than 0");
            }

            var result = await _payrollService.GetWeeklyPayAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            var result = await _employeeService.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }
    }
}
