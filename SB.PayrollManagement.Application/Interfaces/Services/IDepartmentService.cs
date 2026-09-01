using SB.PayrollManagement.Application.Dtos;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IDepartmentService : IBaseService<DepartmentDto, CreateDepartmentDto, CreateDepartmentDto>
    {
    }
}
