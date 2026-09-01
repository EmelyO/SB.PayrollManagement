namespace SB.PayrollManagement.Application.Dtos
{
    public record EmployeeDto
    {
        public int Id { get; init; }
        public int EmployeeTypeId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string SocialSecurityNumber { get; init; } = string.Empty;
        public int? DepartmentId { get; init; }
        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; init; }
    }
}
