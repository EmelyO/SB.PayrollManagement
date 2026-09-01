using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class GovernmentService : BaseService<GovernmentDto, CreateGovernmentDto, CreateGovernmentDto, GovernmentEntities>, IGovernmentService
    {
        public GovernmentService(IGovermentRepository repository,
            ILogger<BaseService<GovernmentDto, CreateGovernmentDto, CreateGovernmentDto, GovernmentEntities>> logger)
            : base(repository, logger)
        {
        }

        protected override GovernmentDto MapToDto(GovernmentEntities entity) => entity.ToDto();
        protected override GovernmentEntities MapToEntity(CreateGovernmentDto dto) => dto.ToEntity();
        protected override void UpdateEntity(CreateGovernmentDto dto, GovernmentEntities entity) => dto.ApplyTo(entity);
    }
}
