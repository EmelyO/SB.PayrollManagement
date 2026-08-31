using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Extentions
{
    public static class GovermentExtention
    {
        public static GovernmentDto ToDto(this GovernmentEntities entity)
        {
            return new GovernmentDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Category = entity.Category,
                StatePower = entity.StatePower,
                Sector = entity.Sector,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static GovernmentEntities ToEntity(this CreateGovernmentDto dto)
        {
            return new GovernmentEntities
            {
                Name = dto.Name,
                Category = dto.Category,
                StatePower = dto.StatePower,
                Sector = dto.Sector,
                CreatedDate = DateTime.UtcNow
            };
        }

        public static void ApplyTo(this CreateGovernmentDto dto, GovernmentEntities entity)
        {
            entity.Name = dto.Name;
            entity.Category = dto.Category;
            entity.StatePower = dto.StatePower;
            entity.Sector = dto.Sector;
            entity.UpdatedDate = DateTime.UtcNow;
        }
    }
}
