using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Base;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IPayrollService
    {
        Task<OperationResult<EmployeePayDto>> GetWeeklyPayAsync(int employeeId);
        Task<OperationResult<PayrollRecordDto>> CreatePayrollRecordAsync(CreatePayrollRecordDto dto);
        Task<OperationResult<List<PayrollRecordDto>>> GetHistoryAsync(int employeeId);
    }
}
