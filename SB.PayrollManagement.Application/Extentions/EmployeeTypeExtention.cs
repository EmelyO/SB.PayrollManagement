using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Extentions
{
    public static class EmployeeTypeExtention
    {
        public static EmployeeTypeDto ToDto(this EmployeeTypes entity)
        {
            return new EmployeeTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static EmployeeTypes ToEntity(this CreateEmployeeTypeDto dto)
        {
            return new EmployeeTypes
            {
                Name = dto.Name
            };
        }

        public static void ApplyTo(this CreateEmployeeTypeDto dto, EmployeeTypes entity)
        {
            entity.Name = dto.Name;
        }
    }
}
