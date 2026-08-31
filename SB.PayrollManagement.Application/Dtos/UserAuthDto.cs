namespace SB.PayrollManagement.Application.Dtos
{
    public record UserAuthDto
    {
        public int UsuarioId { get; init; }
        public string Usuario { get; init; } = string.Empty;
        public string NombreRol { get; init; } = string.Empty;
    }
}
