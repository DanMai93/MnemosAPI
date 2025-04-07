using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(MnemosDbContext dbContext) : base(dbContext)
        {
        }
    }
}
