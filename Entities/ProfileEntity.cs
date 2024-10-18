using Choix_des_technos_et_infras_de_développement___TP1.Models;
using Microsoft.Extensions.Hosting;

namespace Choix_des_technos_et_infras_de_développement___TP1.Entities
{
    public class ProfileEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; } = new List<UserEntity>();
    }
}
