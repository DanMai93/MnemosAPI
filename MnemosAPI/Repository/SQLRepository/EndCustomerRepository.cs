using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class EndCustomerRepository : Repository<EndCustomer>, IEndCustomerRepository
    {
        public EndCustomerRepository(MnemosDbContext dbContext) : base(dbContext) 
        {

        }
    }
}
