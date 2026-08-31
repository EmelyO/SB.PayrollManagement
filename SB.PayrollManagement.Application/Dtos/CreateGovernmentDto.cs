namespace SB.PayrollManagement.Application.Dtos
{
    public record CreateGovernmentDto
    {
        public string Name { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public string StatePower { get; init; } = string.Empty;
        public string Sector { get; init; } = string.Empty;
    }
}
