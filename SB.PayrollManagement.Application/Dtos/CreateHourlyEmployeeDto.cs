namespace SB.PayrollManagement.Application.Dtos
{
    public record CreateHourlyEmployeeDto
    {
        public int EmployeeId { get; init; }
        public decimal HourlyRate { get; init; }
    }
}
