using SB.PayrollManagement.Application.Dtos;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IEmployeeTypeService : IBaseService<EmployeeTypeDto, CreateEmployeeTypeDto, CreateEmployeeTypeDto>
    {
    }
}
