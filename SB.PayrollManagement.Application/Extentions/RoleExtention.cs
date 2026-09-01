using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Extentions
{
    public static class RoleExtention
    {
        public static RoleDto ToDto(this Roles entity)
        {
            return new RoleDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
