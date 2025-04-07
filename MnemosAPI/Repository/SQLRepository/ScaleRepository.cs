using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class ScaleRepository : Repository<Scale>, IScaleRepository
    {
        public ScaleRepository(MnemosDbContext dbContext) : base(dbContext)
        {
        }
    }
}
