namespace SB.PayrollManagement.Application.Dtos
{
    public record CreateEmployeeDto
    {
        public int EmployeeTypeId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string SocialSecurityNumber { get; init; } = string.Empty;
        public int? DepartmentId { get; init; }
    }
}
