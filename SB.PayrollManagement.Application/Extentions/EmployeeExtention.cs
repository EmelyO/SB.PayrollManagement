using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Extentions
{
    public static class EmployeeExtention
    {
        public static EmployeeDto ToDto(this Employees entity)
        {
            return new EmployeeDto
            {
                Id = entity.Id,
                EmployeeTypeId = entity.EmployeeTypeId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                SocialSecurityNumber = entity.SocialSecurityNumber,
                DepartmentId = entity.DepartmentId,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static Employees ToEntity(this CreateEmployeeDto dto)
        {
            return new Employees
            {
                EmployeeTypeId = dto.EmployeeTypeId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                SocialSecurityNumber = dto.SocialSecurityNumber,
                DepartmentId = dto.DepartmentId,
                CreatedDate = DateTime.UtcNow
            };
        }

        public static void ApplyTo(this CreateEmployeeDto dto, Employees entity)
        {
            entity.EmployeeTypeId = dto.EmployeeTypeId;
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.SocialSecurityNumber = dto.SocialSecurityNumber;
            entity.DepartmentId = dto.DepartmentId;
            entity.UpdatedDate = DateTime.UtcNow;
        }
    }
}
