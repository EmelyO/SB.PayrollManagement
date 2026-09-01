using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class SalariedEmployeeService : BaseService<SalariedEmployeeDto, CreateSalariedEmployeeDto, UpdateSalariedEmployeeDto, SalariedEmployees>, ISalariedEmployeeService
    {
        public SalariedEmployeeService(ISalariedEmployeeRepository repository,
            ILogger<BaseService<SalariedEmployeeDto, CreateSalariedEmployeeDto, UpdateSalariedEmployeeDto, SalariedEmployees>> logger)
            : base(repository, logger)
        {
        }

        protected override SalariedEmployeeDto MapToDto(SalariedEmployees entity) =>
            entity.ToDto() with { WeeklyPay = CalculateWeeklyPay(entity.WeeklySalary) };

        protected override SalariedEmployees MapToEntity(CreateSalariedEmployeeDto dto) => dto.ToEntity();
        protected override void UpdateEntity(UpdateSalariedEmployeeDto dto, SalariedEmployees entity) => dto.ApplyTo(entity);
        public static decimal CalculateWeeklyPay(decimal weeklySalary) => weeklySalary;
    }
}
