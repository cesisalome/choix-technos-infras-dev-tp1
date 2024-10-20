using Choix_des_technos_et_infras_de_développement___TP1.Persistence;

namespace Choix_des_technos_et_infras_de_développement___TP1.Application
{
    public class UseCaseBase
    {
        public TP1Context _dbContext { get; set; }

        public UseCaseBase(TP1Context dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
