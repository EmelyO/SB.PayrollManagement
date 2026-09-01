using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class EmployeeService : BaseService<EmployeeDto, CreateEmployeeDto, CreateEmployeeDto, Employees>, IEmployeeService
    {
        public EmployeeService(IEmployeeRepository repository,
            ILogger<BaseService<EmployeeDto, CreateEmployeeDto, CreateEmployeeDto, Employees>> logger)
            : base(repository, logger)
        {
        }

        protected override EmployeeDto MapToDto(Employees entity) => entity.ToDto();
        protected override Employees MapToEntity(CreateEmployeeDto dto) => dto.ToEntity();
        protected override void UpdateEntity(CreateEmployeeDto dto, Employees entity) => dto.ApplyTo(entity);
    }
}
