using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class SoftSkillRepository: Repository<SoftSkill>, ISoftSkillRepository
    {
        public SoftSkillRepository(MnemosDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
