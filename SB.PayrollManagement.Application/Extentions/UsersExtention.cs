using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Extentions
{
    public static class UsersExtention
    {
        public static UserAuthDto ToUsersDtoFromEntity(this Users user, string roleName)
        {
            return new UserAuthDto
            {
                UsuarioId = user.Id,
                Usuario = user.Username,
                NombreRol = roleName
            };
        }

        public static UserDto ToDto(this Users user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                RoleId = user.RoleId,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };
        }

        public static Users ToEntity(this CreateUserDto dto)
        {
            return new Users
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId,
                CreatedDate = DateTime.UtcNow
            };
        }

        public static void ApplyTo(this CreateUserDto dto, Users user)
        {
            user.Username = dto.Username;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.RoleId = dto.RoleId;
            user.UpdatedDate = DateTime.UtcNow;
        }
    }
}
