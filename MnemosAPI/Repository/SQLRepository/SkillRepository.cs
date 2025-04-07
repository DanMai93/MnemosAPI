using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(MnemosDbContext dbContext) : base(dbContext)
        {
        }
    }
}
