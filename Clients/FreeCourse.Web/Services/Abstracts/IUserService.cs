using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Abstracts;

public interface IUserService
{
    Task<UserViewModel> GetUser();
}