using Microsoft.EntityFrameworkCore;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Domain.Entities;
using SB.PayrollManagement.Persistence.Base;
using SB.PayrollManagement.Persistence.Context;

namespace SB.PayrollManagement.Persistence.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        public UsersRepository(PayrollManagementContext context): base(context)
        {
            
        }

        public async Task<Users?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
