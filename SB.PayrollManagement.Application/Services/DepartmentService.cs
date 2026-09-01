using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class DepartmentService : BaseService<DepartmentDto, CreateDepartmentDto, CreateDepartmentDto, Departments>, IDepartmentService
    {
        public DepartmentService(IDepartmentRepository repository,
            ILogger<BaseService<DepartmentDto, CreateDepartmentDto, CreateDepartmentDto, Departments>> logger)
            : base(repository, logger)
        {
        }

        protected override DepartmentDto MapToDto(Departments entity) => entity.ToDto();
        protected override Departments MapToEntity(CreateDepartmentDto dto) => dto.ToEntity();
        protected override void UpdateEntity(CreateDepartmentDto dto, Departments entity) => dto.ApplyTo(entity);
    }
}
