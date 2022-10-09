namespace ECommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentications
    {
        Task<Dtos.Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
        Task<Dtos.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}
