using YayinEviApi.Application.Repositories.IRezervationR;
using YayinEviApi.Domain.Entities.RezervationE;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence.Repositories.RezervationR
{
    public class RezervationRepository : GeneralRepository<Rezervation>, IRezervationRepository
    {
        public RezervationRepository(YayinEviApiDbContext context) : base(context)
        {
        }
    }
}
