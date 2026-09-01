using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class SalariedCommissionEmployeeService : BaseService<SalariedCommissionEmployeeDto, CreateSalariedCommissionEmployeeDto, UpdateSalariedCommissionEmployeeDto, SalariedCommissionEmployees>, ISalariedCommissionEmployeeService
    {
        public SalariedCommissionEmployeeService(ISalariedCommissionEmployeeRepository repository,
            ILogger<BaseService<SalariedCommissionEmployeeDto, CreateSalariedCommissionEmployeeDto, UpdateSalariedCommissionEmployeeDto, SalariedCommissionEmployees>> logger)
            : base(repository, logger)
        {
        }

        protected override SalariedCommissionEmployeeDto MapToDto(SalariedCommissionEmployees entity) => entity.ToDto();

        protected override SalariedCommissionEmployees MapToEntity(CreateSalariedCommissionEmployeeDto dto) => dto.ToEntity();
        protected override void UpdateEntity(UpdateSalariedCommissionEmployeeDto dto, SalariedCommissionEmployees entity) => dto.ApplyTo(entity);
        public static decimal CalculateWeeklyPay(decimal grossSales, decimal commissionRate, decimal baseSalary) =>
            (grossSales * commissionRate) + baseSalary + (baseSalary * 0.10m);
    }
}
