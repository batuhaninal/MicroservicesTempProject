using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;

namespace FreeCourse.Web.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<List<CourseViewModel>> GetAllCourseAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<CourseViewModel>> GetAllCourseByUserIdAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CourseViewModel> GetByCourseId(string courseId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateCourseAsync(CourseCreateInput model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCourseAsync(CourseUpdateInput model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCourseAsync(string courseId)
    {
        throw new NotImplementedException();
    }
}