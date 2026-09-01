using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Extentions
{
    public static class PayrollExtention
    {
        // Solo mapeo de campos. El cálculo de WeeklyPay vive en cada Service.
        public static SalariedEmployeeDto ToDto(this SalariedEmployees entity)
        {
            return new SalariedEmployeeDto
            {
                EmployeeId = entity.EmployeeId,
                WeeklySalary = entity.WeeklySalary
            };
        }

        public static SalariedEmployees ToEntity(this CreateSalariedEmployeeDto dto)
        {
            return new SalariedEmployees
            {
                EmployeeId = dto.EmployeeId,
                WeeklySalary = dto.WeeklySalary
            };
        }

        public static void ApplyTo(this UpdateSalariedEmployeeDto dto, SalariedEmployees entity)
        {
            entity.WeeklySalary = dto.WeeklySalary;
        }

        public static HourlyEmployeeDto ToDto(this HourlyEmployees entity)
        {
            return new HourlyEmployeeDto
            {
                EmployeeId = entity.EmployeeId,
                HourlyRate = entity.HourlyRate
            };
        }

        public static HourlyEmployees ToEntity(this CreateHourlyEmployeeDto dto)
        {
            return new HourlyEmployees
            {
                EmployeeId = dto.EmployeeId,
                HourlyRate = dto.HourlyRate
            };
        }

        public static void ApplyTo(this UpdateHourlyEmployeeDto dto, HourlyEmployees entity)
        {
            entity.HourlyRate = dto.HourlyRate;
        }

        public static CommissionEmployeeDto ToDto(this CommissionEmployees entity)
        {
            return new CommissionEmployeeDto
            {
                EmployeeId = entity.EmployeeId,
                CommissionRate = entity.CommissionRate
            };
        }

        public static CommissionEmployees ToEntity(this CreateCommissionEmployeeDto dto)
        {
            return new CommissionEmployees
            {
                EmployeeId = dto.EmployeeId,
                CommissionRate = dto.CommissionRate
            };
        }

        public static void ApplyTo(this UpdateCommissionEmployeeDto dto, CommissionEmployees entity)
        {
            entity.CommissionRate = dto.CommissionRate;
        }

        public static SalariedCommissionEmployeeDto ToDto(this SalariedCommissionEmployees entity)
        {
            return new SalariedCommissionEmployeeDto
            {
                EmployeeId = entity.EmployeeId,
                CommissionRate = entity.CommissionRate,
                BaseSalary = entity.BaseSalary
            };
        }

        public static SalariedCommissionEmployees ToEntity(this CreateSalariedCommissionEmployeeDto dto)
        {
            return new SalariedCommissionEmployees
            {
                EmployeeId = dto.EmployeeId,
                CommissionRate = dto.CommissionRate,
                BaseSalary = dto.BaseSalary
            };
        }

        public static void ApplyTo(this UpdateSalariedCommissionEmployeeDto dto, SalariedCommissionEmployees entity)
        {
            entity.CommissionRate = dto.CommissionRate;
            entity.BaseSalary = dto.BaseSalary;
        }

        public static PayrollRecordDto ToDto(this PayrollRecords entity)
        {
            return new PayrollRecordDto
            {
                Id = entity.Id,
                EmployeeId = entity.EmployeeId,
                WeekStartDate = entity.WeekStartDate,
                WeekEndDate = entity.WeekEndDate,
                HoursWorked = entity.HoursWorked,
                GrossSales = entity.GrossSales,
                CalculatedPay = entity.CalculatedPay,
                CreatedDate = entity.CreatedDate
            };
        }

        public static PayrollRecords ToEntity(this CreatePayrollRecordDto dto, decimal calculatedPay)
        {
            return new PayrollRecords
            {
                EmployeeId = dto.EmployeeId,
                WeekStartDate = dto.WeekStartDate,
                WeekEndDate = dto.WeekEndDate,
                HoursWorked = dto.HoursWorked,
                GrossSales = dto.GrossSales,
                CalculatedPay = calculatedPay,
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
