using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class HourlyEmployeeService : BaseService<HourlyEmployeeDto, CreateHourlyEmployeeDto, UpdateHourlyEmployeeDto, HourlyEmployees>, IHourlyEmployeeService
    {
        public HourlyEmployeeService(IHourlyEmployeeRepository repository,
            ILogger<BaseService<HourlyEmployeeDto, CreateHourlyEmployeeDto, UpdateHourlyEmployeeDto, HourlyEmployees>> logger)
            : base(repository, logger)
        {
        }

        protected override HourlyEmployeeDto MapToDto(HourlyEmployees entity) => entity.ToDto();

        protected override HourlyEmployees MapToEntity(CreateHourlyEmployeeDto dto) => dto.ToEntity();
        protected override void UpdateEntity(UpdateHourlyEmployeeDto dto, HourlyEmployees entity) => dto.ApplyTo(entity);
        public static decimal CalculateWeeklyPay(decimal hourlyRate, decimal hoursWorked)
        {
            if (hoursWorked <= 40)
            {
                return hourlyRate * hoursWorked;
            }

            return (hourlyRate * 40) + (hourlyRate * 1.5m * (hoursWorked - 40));
        }
    }
}
