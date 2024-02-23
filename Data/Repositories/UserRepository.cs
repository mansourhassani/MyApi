using Common.Utilities;
using Data.Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {
            var PasswordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(x => x.UserName == username && x.PasswordHash == PasswordHash).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
