using Microsoft.Extensions.Logging;
using static SB.PayrollManagement.Application.Constants.EmployeeTypeNames;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Base;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeTypeRepository _employeeTypeRepository;
        private readonly ISalariedEmployeeRepository _salariedEmployeeRepository;
        private readonly IHourlyEmployeeRepository _hourlyEmployeeRepository;
        private readonly ICommissionEmployeeRepository _commissionEmployeeRepository;
        private readonly ISalariedCommissionEmployeeRepository _salariedCommissionEmployeeRepository;
        private readonly IPayrollRecordRepository _payrollRecordRepository;
        private readonly ILogger<PayrollService> _logger;

        public PayrollService(IEmployeeRepository employeeRepository,
            IEmployeeTypeRepository employeeTypeRepository,
            ISalariedEmployeeRepository salariedEmployeeRepository,
            IHourlyEmployeeRepository hourlyEmployeeRepository,
            ICommissionEmployeeRepository commissionEmployeeRepository,
            ISalariedCommissionEmployeeRepository salariedCommissionEmployeeRepository,
            IPayrollRecordRepository payrollRecordRepository,
            ILogger<PayrollService> logger)
        {
            _employeeRepository = employeeRepository;
            _employeeTypeRepository = employeeTypeRepository;
            _salariedEmployeeRepository = salariedEmployeeRepository;
            _hourlyEmployeeRepository = hourlyEmployeeRepository;
            _commissionEmployeeRepository = commissionEmployeeRepository;
            _salariedCommissionEmployeeRepository = salariedCommissionEmployeeRepository;
            _payrollRecordRepository = payrollRecordRepository;
            _logger = logger;
        }

        public async Task<OperationResult<EmployeePayDto>> GetWeeklyPayAsync(int employeeId)
        {
            try
            {
                if (employeeId <= 0)
                {
                    return OperationResult<EmployeePayDto>.Failure("The ID must be greater than 0");
                }

                var employeeType = await GetEmployeeTypeAsync(employeeId);
                if (employeeType is null)
                {
                    return OperationResult<EmployeePayDto>.Failure($"No employee found with ID {employeeId}, or its employee type is not configured");
                }

                var latestRecord = await _payrollRecordRepository.GetLatestByEmployeeIdAsync(employeeId);
                if (latestRecord is null)
                {
                    return OperationResult<EmployeePayDto>.Failure("No payroll record has been captured for this employee yet");
                }

                return OperationResult<EmployeePayDto>.Success("Weekly pay retrieved successfully", new EmployeePayDto
                {
                    EmployeeId = employeeId,
                    EmployeeType = employeeType.Name,
                    WeeklyPay = latestRecord.CalculatedPay
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving weekly pay for employee {EmployeeId}", employeeId);
                return OperationResult<EmployeePayDto>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult<PayrollRecordDto>> CreatePayrollRecordAsync(CreatePayrollRecordDto dto)
        {
            try
            {
                if (dto.EmployeeId <= 0)
                {
                    return OperationResult<PayrollRecordDto>.Failure("The employee ID must be greater than 0");
                }
                if (dto.WeekEndDate < dto.WeekStartDate)
                {
                    return OperationResult<PayrollRecordDto>.Failure("WeekEndDate cannot be before WeekStartDate");
                }

                var employeeType = await GetEmployeeTypeAsync(dto.EmployeeId);
                if (employeeType is null)
                {
                    return OperationResult<PayrollRecordDto>.Failure($"No employee found with ID {dto.EmployeeId}, or its employee type is not configured");
                }

                decimal calculatedPay;

                switch (employeeType.Name)
                {
                    case Salaried:
                        {
                            var rateResult = await _salariedEmployeeRepository.GetByIdAsync(dto.EmployeeId);
                            if (!rateResult.IsSuccess || rateResult.Data is null)
                            {
                                return OperationResult<PayrollRecordDto>.Failure("Salary data has not been captured for this employee");
                            }
                            SalariedEmployees rate = rateResult.Data;
                            calculatedPay = SalariedEmployeeService.CalculateWeeklyPay(rate.WeeklySalary);
                            break;
                        }
                    case Hourly:
                        {
                            if (dto.HoursWorked is null)
                            {
                                return OperationResult<PayrollRecordDto>.Failure("HoursWorked is required for hourly employees");
                            }
                            var rateResult = await _hourlyEmployeeRepository.GetByIdAsync(dto.EmployeeId);
                            if (!rateResult.IsSuccess || rateResult.Data is null)
                            {
                                return OperationResult<PayrollRecordDto>.Failure("Hourly rate has not been captured for this employee");
                            }
                            HourlyEmployees rate = rateResult.Data;
                            calculatedPay = HourlyEmployeeService.CalculateWeeklyPay(rate.HourlyRate, dto.HoursWorked.Value);
                            break;
                        }
                    case Commission:
                        {
                            if (dto.GrossSales is null)
                            {
                                return OperationResult<PayrollRecordDto>.Failure("GrossSales is required for commission employees");
                            }
                            var rateResult = await _commissionEmployeeRepository.GetByIdAsync(dto.EmployeeId);
                            if (!rateResult.IsSuccess || rateResult.Data is null)
                            {
                                return OperationResult<PayrollRecordDto>.Failure("Commission rate has not been captured for this employee");
                            }
                            CommissionEmployees rate = rateResult.Data;
                            calculatedPay = CommissionEmployeeService.CalculateWeeklyPay(dto.GrossSales.Value, rate.CommissionRate);
                            break;
                        }
                    case SalariedCommission:
                        {
                            if (dto.GrossSales is null)
                            {
                                return OperationResult<PayrollRecordDto>.Failure("GrossSales is required for this employee type");
                            }
                            var rateResult = await _salariedCommissionEmployeeRepository.GetByIdAsync(dto.EmployeeId);
                            if (!rateResult.IsSuccess || rateResult.Data is null)
                            {
                                return OperationResult<PayrollRecordDto>.Failure("Salary/commission data has not been captured for this employee");
                            }
                            SalariedCommissionEmployees rate = rateResult.Data;
                            calculatedPay = SalariedCommissionEmployeeService.CalculateWeeklyPay(dto.GrossSales.Value, rate.CommissionRate, rate.BaseSalary);
                            break;
                        }
                    default:
                        return OperationResult<PayrollRecordDto>.Failure($"Unknown employee type: {employeeType.Name}");
                }

                var record = dto.ToEntity(calculatedPay);

                var addResult = await _payrollRecordRepository.AddAsync(record);
                if (!addResult.IsSuccess || addResult.Data is null)
                {
                    return OperationResult<PayrollRecordDto>.Failure(addResult.Message ?? "Error saving the payroll record");
                }

                PayrollRecords saved = addResult.Data;
                return OperationResult<PayrollRecordDto>.Success("Payroll record created successfully", saved.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payroll record for employee {EmployeeId}", dto.EmployeeId);
                return OperationResult<PayrollRecordDto>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult<List<PayrollRecordDto>>> GetHistoryAsync(int employeeId)
        {
            try
            {
                if (employeeId <= 0)
                {
                    return OperationResult<List<PayrollRecordDto>>.Failure("The ID must be greater than 0");
                }

                var result = await _payrollRecordRepository.GetAllAsync(p => p.EmployeeId == employeeId);
                if (!result.IsSuccess || result.Data is null)
                {
                    return OperationResult<List<PayrollRecordDto>>.Failure(result.Message ?? "No records found");
                }

                List<PayrollRecords> records = result.Data;
                var dtos = records
                    .OrderByDescending(r => r.WeekEndDate)
                    .Select(r => r.ToDto())
                    .ToList();

                return OperationResult<List<PayrollRecordDto>>.Success("History retrieved successfully", dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payroll history for employee {EmployeeId}", employeeId);
                return OperationResult<List<PayrollRecordDto>>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult<List<WeeklyReportItemDto>>> GetWeeklyReportAsync(DateOnly weekStartDate)
        {
            try
            {
                var recordsResult = await _payrollRecordRepository.GetAllAsync(p => p.WeekStartDate == weekStartDate);
                if (!recordsResult.IsSuccess || recordsResult.Data is null)
                {
                    return OperationResult<List<WeeklyReportItemDto>>.Failure(recordsResult.Message ?? "No records found");
                }

                List<PayrollRecords> records = recordsResult.Data;
                if (records.Count == 0)
                {
                    return OperationResult<List<WeeklyReportItemDto>>.Success("No payroll records for this week", new List<WeeklyReportItemDto>());
                }

                var employeeIds = records.Select(r => r.EmployeeId).Distinct().ToList();
                var employeesResult = await _employeeRepository.GetAllAsync(e => employeeIds.Contains(e.Id));
                List<Employees> employees = employeesResult.IsSuccess && employeesResult.Data is not null
                    ? employeesResult.Data
                    : new List<Employees>();
                var employeesById = employees.ToDictionary(e => e.Id);

                var typeIds = employees.Select(e => e.EmployeeTypeId).Distinct().ToList();
                var typesResult = await _employeeTypeRepository.GetAllAsync(t => typeIds.Contains(t.Id));
                List<EmployeeTypes> types = typesResult.IsSuccess && typesResult.Data is not null
                    ? typesResult.Data
                    : new List<EmployeeTypes>();
                var typesById = types.ToDictionary(t => t.Id);

                var report = records
                    .Select(r =>
                    {
                        employeesById.TryGetValue(r.EmployeeId, out var employee);
                        var employeeTypeName = string.Empty;
                        if (employee is not null && typesById.TryGetValue(employee.EmployeeTypeId, out var type))
                        {
                            employeeTypeName = type.Name;
                        }

                        return new WeeklyReportItemDto
                        {
                            EmployeeId = r.EmployeeId,
                            EmployeeName = employee is not null ? $"{employee.FirstName} {employee.LastName}" : "Unknown",
                            EmployeeType = employeeTypeName,
                            HoursWorked = r.HoursWorked,
                            GrossSales = r.GrossSales,
                            CalculatedPay = r.CalculatedPay
                        };
                    })
                    .OrderBy(r => r.EmployeeName)
                    .ToList();

                return OperationResult<List<WeeklyReportItemDto>>.Success("Weekly report generated successfully", report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating weekly report for week {WeekStartDate}", weekStartDate);
                return OperationResult<List<WeeklyReportItemDto>>.Failure($"Error: {ex.Message}");
            }
        }

        private async Task<EmployeeTypes?> GetEmployeeTypeAsync(int employeeId)
        {
            var employeeResult = await _employeeRepository.GetByIdAsync(employeeId);
            if (!employeeResult.IsSuccess || employeeResult.Data is null)
            {
                return null;
            }

            Employees employee = employeeResult.Data;

            var typeResult = await _employeeTypeRepository.GetByIdAsync(employee.EmployeeTypeId);
            if (!typeResult.IsSuccess || typeResult.Data is null)
            {
                return null;
            }

            return typeResult.Data;
        }
    }
}
