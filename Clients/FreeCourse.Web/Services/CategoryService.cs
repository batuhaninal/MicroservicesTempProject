using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;

namespace FreeCourse.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<List<CategoryViewModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}