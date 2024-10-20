using Choix_des_technos_et_infras_de_développement___TP1.Persistence;

namespace Choix_des_technos_et_infras_de_développement___TP1.Application.User.Commands
{
    public class DeleteUserCommand : UseCaseBase
    {
        public DeleteUserCommand(TP1Context dbContext) : base(dbContext) { }

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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
