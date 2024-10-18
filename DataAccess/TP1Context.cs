using Choix_des_technos_et_infras_de_développement___TP1.Entities;
using Microsoft.EntityFrameworkCore;

namespace Choix_des_technos_et_infras_de_développement___TP1.DataAccess
{
    public class TP1Context : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public TP1Context(DbContextOptions<TP1Context> options) : base(options)
        {
        }
    }
}
