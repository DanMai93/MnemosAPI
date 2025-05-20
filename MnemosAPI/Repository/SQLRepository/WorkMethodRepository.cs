using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class WorkMethodRepository: Repository<WorkMethod>, IWorkMethodRepository
    {
        public WorkMethodRepository(MnemosDbContext dbContext): base(dbContext)
        {
            
        }
    }
}
