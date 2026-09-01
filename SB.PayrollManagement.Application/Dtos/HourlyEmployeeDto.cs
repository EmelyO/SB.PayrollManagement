namespace SB.PayrollManagement.Application.Dtos
{
    public record HourlyEmployeeDto
    {
        public int EmployeeId { get; init; }
        public decimal HourlyRate { get; init; }
    }
}
