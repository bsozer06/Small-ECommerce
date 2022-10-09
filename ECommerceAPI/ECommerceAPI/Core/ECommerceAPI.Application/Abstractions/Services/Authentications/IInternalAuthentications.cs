namespace ECommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentications
    {
        Task<Dtos.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);

    }
}
