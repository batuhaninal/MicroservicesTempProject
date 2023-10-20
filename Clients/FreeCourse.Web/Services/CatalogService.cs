using FreeCourse.Shared.Dtos;
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

    public async Task<List<CourseViewModel>> GetAllCourseAsync()
    {
        var response = await _httpClient.GetAsync("courses");

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync <Response<List<CourseViewModel>>>();

        return result.Data;
    }

    public async Task<List<CategoryViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("categories");

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync <Response<List<CategoryViewModel>>>();

        return result.Data;
    }

    public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync <Response<List<CourseViewModel>>>();

        return result.Data;
    }

    public async Task<CourseViewModel> GetByCourseId(string courseId)
    {
        var response = await _httpClient.GetAsync($"courses/{courseId}");

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

        return result.Data;
    }

    public async Task<bool> CreateCourseAsync(CourseCreateInput model)
    {
        var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses", model);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(CourseUpdateInput model)
    {
        var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", model);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCourseAsync(string courseId)
    {
        var response = await _httpClient.DeleteAsync($"courses/{courseId}");

        return response.IsSuccessStatusCode;
    }
}