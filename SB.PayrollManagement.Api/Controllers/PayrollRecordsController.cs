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
    public class PayrollRecordsController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollRecordsController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpGet("{employeeId:int}")]
        public async Task<IActionResult> GetHistory(int employeeId)
        {
            if (employeeId <= 0)
            {
                return BadRequest("The ID must be greater than 0");
            }

            var result = await _payrollService.GetHistoryAsync(employeeId);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] CreatePayrollRecordDto dto)
        {
            var result = await _payrollService.CreatePayrollRecordAsync(dto);
            if (!result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return Ok(result);
        }
    }
}
