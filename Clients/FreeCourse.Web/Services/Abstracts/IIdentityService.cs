using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using IdentityModel.Client;

namespace FreeCourse.Web.Services.Abstracts;

public interface IIdentityService
{
    Task<Response<bool>> SignInAsync(SigninInput signinInput);
    Task<TokenResponse> GetAccessTokenByRefreshTokenAsync();
    Task RevokeRefreshTokenAsync();
}