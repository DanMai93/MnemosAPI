using MnemosAPI.Data;

namespace MnemosAPI.Repository.SQLRepository
{
    public class ArchitectureRepository : Repository<Models.Architecture>, IArchitectureRepository
    {
        public ArchitectureRepository(MnemosDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
