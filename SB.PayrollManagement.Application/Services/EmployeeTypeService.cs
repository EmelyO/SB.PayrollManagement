using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class EmployeeTypeService : BaseService<EmployeeTypeDto, CreateEmployeeTypeDto, CreateEmployeeTypeDto, EmployeeTypes>, IEmployeeTypeService
    {
        public EmployeeTypeService(IEmployeeTypeRepository repository,
            ILogger<BaseService<EmployeeTypeDto, CreateEmployeeTypeDto, CreateEmployeeTypeDto, EmployeeTypes>> logger)
            : base(repository, logger)
        {
        }

        protected override EmployeeTypeDto MapToDto(EmployeeTypes entity) => entity.ToDto();
        protected override EmployeeTypes MapToEntity(CreateEmployeeTypeDto dto) => dto.ToEntity();
        protected override void UpdateEntity(CreateEmployeeTypeDto dto, EmployeeTypes entity) => dto.ApplyTo(entity);
    }
}
