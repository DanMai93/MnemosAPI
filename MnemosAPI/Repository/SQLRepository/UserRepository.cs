using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MnemosDbContext dbContext) : base(dbContext)
        {
        }
    }
}
