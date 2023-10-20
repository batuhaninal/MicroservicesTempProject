namespace FreeCourse.Web.Services.Abstracts;

public interface IClientCredentialTokenService
{
    Task<string> GetToken();
}