using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class CommissionEmployeeService : BaseService<CommissionEmployeeDto, CreateCommissionEmployeeDto, UpdateCommissionEmployeeDto, CommissionEmployees>, ICommissionEmployeeService
    {
        public CommissionEmployeeService(ICommissionEmployeeRepository repository,
            ILogger<BaseService<CommissionEmployeeDto, CreateCommissionEmployeeDto, UpdateCommissionEmployeeDto, CommissionEmployees>> logger)
            : base(repository, logger)
        {
        }

        protected override CommissionEmployeeDto MapToDto(CommissionEmployees entity) => entity.ToDto();

        protected override CommissionEmployees MapToEntity(CreateCommissionEmployeeDto dto) => dto.ToEntity();
        protected override void UpdateEntity(UpdateCommissionEmployeeDto dto, CommissionEmployees entity) => dto.ApplyTo(entity);
        public static decimal CalculateWeeklyPay(decimal grossSales, decimal commissionRate) => grossSales * commissionRate;
    }
}
