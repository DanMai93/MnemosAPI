using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class ManagementToolRepository: Repository<ManagementTool>, IManagementToolRepository
    {
        public ManagementToolRepository(MnemosDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
