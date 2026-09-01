using SB.PayrollManagement.Application.Dtos;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IHourlyEmployeeService : IBaseService<HourlyEmployeeDto, CreateHourlyEmployeeDto, UpdateHourlyEmployeeDto>
    {
    }
}
