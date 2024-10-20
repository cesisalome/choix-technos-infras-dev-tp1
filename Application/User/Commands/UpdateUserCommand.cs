using Choix_des_technos_et_infras_de_développement___TP1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Choix_des_technos_et_infras_de_développement___TP1.Application.User.Commands
{
    public class UpdateUserCommand : UseCaseBase
    {
        public UpdateUserCommand(TP1Context dbContext) : base(dbContext) { }

        public async Task UpdateUserAsync(int id, UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userToUpdate = await _dbContext.Users.FindAsync(id, cancellationToken);

                if (userToUpdate == null)
                {
                    throw new Exception(string.Format("User not found : {0}", user));
                }

                var profile = await _dbContext.Profiles
                    .Where(profile => profile.Name == user.ProfileName)
                    .FirstOrDefaultAsync(cancellationToken);

                if (profile == null)
                {
                    throw new Exception(string.Format("Profile not found : {0}", user.ProfileName));
                }

                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.PhoneNumber = user.PhoneNumber;
                userToUpdate.Email = user.Email;
                userToUpdate.Profile = profile;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
