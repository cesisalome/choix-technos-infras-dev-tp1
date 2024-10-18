using Choix_des_technos_et_infras_de_développement___TP1.DataAccess;
using Choix_des_technos_et_infras_de_développement___TP1.Entities;
using Choix_des_technos_et_infras_de_développement___TP1.Models;
using Microsoft.EntityFrameworkCore;

namespace Choix_des_technos_et_infras_de_développement___TP1.Services
{
    public class UserService
    {
        private TP1Context _dbContext { get; set; }

        public UserService(TP1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(user => user.Profile)
                    .Where(user => user.Id == userId)
                    .Select(user => new UserModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        ProfileName = user.Profile.Name
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new Exception(string.Format("User not found : {0}", userId));
                }

                return user;
            }
            catch (Exception) {
                throw;
            }
        }

        public async Task AddUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _dbContext.Profiles
                    .Where(profile => profile.Name == user.ProfileName)
                    .FirstOrDefaultAsync(cancellationToken);

                if (profile == null) {
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
            catch (Exception) {
                throw;
            }
        }

        public async Task DeleteUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            try
            {
                var userToDelete = await _dbContext.Users.FindAsync(userId, cancellationToken);

                if (userToDelete == null)
                {
                    throw new Exception(string.Format("User not found : {0}", userId));
                }

                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception) {
                throw;
            }
        }
    }
}
