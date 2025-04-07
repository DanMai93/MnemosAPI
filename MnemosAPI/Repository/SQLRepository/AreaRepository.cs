using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(MnemosDbContext dbContext) : base(dbContext)
        {
        }
    }
}
