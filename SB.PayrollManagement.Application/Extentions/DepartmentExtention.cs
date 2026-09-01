using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Extentions
{
    public static class DepartmentExtention
    {
        public static DepartmentDto ToDto(this Departments entity)
        {
            return new DepartmentDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static Departments ToEntity(this CreateDepartmentDto dto)
        {
            return new Departments
            {
                Name = dto.Name
            };
        }

        public static void ApplyTo(this CreateDepartmentDto dto, Departments entity)
        {
            entity.Name = dto.Name;
        }
    }
}
