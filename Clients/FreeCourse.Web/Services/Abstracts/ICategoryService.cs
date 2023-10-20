using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Abstracts;

public interface ICategoryService
{
    Task<List<CategoryViewModel>> GetAllAsync();
}