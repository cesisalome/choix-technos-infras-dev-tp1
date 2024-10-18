using Choix_des_technos_et_infras_de_développement___TP1.Domain;
using Microsoft.EntityFrameworkCore;

namespace Choix_des_technos_et_infras_de_développement___TP1.Persistence
{
    public class TP1Context : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }

        public TP1Context(DbContextOptions<TP1Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileEntity>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Profile)
                .HasForeignKey(e => e.ProfileId)
                .IsRequired();

            modelBuilder.Entity<UserEntity>()
                .HasOne(e => e.Profile)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.ProfileId)
                .IsRequired();
        }
    }
}
