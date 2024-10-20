using Choix_des_technos_et_infras_de_développement___TP1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Choix_des_technos_et_infras_de_développement___TP1.Application.User.Queries
{
    public class GetUserQuery : UseCaseBase
    {
        public GetUserQuery(TP1Context dbContext) : base(dbContext) { }

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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
