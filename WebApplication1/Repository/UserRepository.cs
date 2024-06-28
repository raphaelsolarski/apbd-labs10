using WebApplication1.Context;

namespace WebApplication1.Repository;

public class UserRepository: IUserRepository
{
    public AppUser CreateUser(PrescriptionsContext context, AppUser appUser)
    {
        context.AppUsers.Add(appUser);
        return appUser;
    }

    public AppUser? FindUserByLogin(PrescriptionsContext context, string login)
    {
        return context.AppUsers.SingleOrDefault(u => u.Login == login);
    }

    public AppUser? FindUserByRefreshToken(PrescriptionsContext context, string refreshToken)
    {
        return context.AppUsers.SingleOrDefault(u => u.RefreshToken == refreshToken);
    }
}