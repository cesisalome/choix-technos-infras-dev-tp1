using Choix_des_technos_et_infras_de_développement___TP1.Domain;
using Choix_des_technos_et_infras_de_développement___TP1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Choix_des_technos_et_infras_de_développement___TP1.Application.User.Commands
{
    public class AddUserCommand : UseCaseBase
    {
        public AddUserCommand(TP1Context dbContext) : base(dbContext) { }

        public async Task AddUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _dbContext.Profiles
                    .Where(profile => profile.Name == user.ProfileName)
                    .FirstOrDefaultAsync(cancellationToken);

                if (profile == null)
                {
                    throw new Exception(string.Format("Profile not found : {0}", user.ProfileName));
                }

                var userToAdd = new UserEntity
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Profile = profile,
                };

                await _dbContext.Users.AddAsync(userToAdd, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
