namespace SB.PayrollManagement.Application.Dtos
{
    public record UpdateHourlyEmployeeDto
    {
        public decimal HourlyRate { get; init; }
    }
}
