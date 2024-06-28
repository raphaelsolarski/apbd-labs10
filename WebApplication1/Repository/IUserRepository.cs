using WebApplication1.Context;

namespace WebApplication1.Repository;

public interface IUserRepository
{
    AppUser CreateUser(PrescriptionsContext context, AppUser appUser);
    AppUser? FindUserByLogin(PrescriptionsContext context, string login);
    AppUser? FindUserByRefreshToken(PrescriptionsContext context, string refreshToken);
}