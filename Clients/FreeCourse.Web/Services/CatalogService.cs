using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;

namespace FreeCourse.Web.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;
    private readonly IPhotoStockService _photoStockService;
    private readonly PhotoHelper _photoHelper;

    public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
    {
        _httpClient = httpClient;
        _photoStockService = photoStockService;
        _photoHelper = photoHelper;
    }

    public async Task<List<CourseViewModel>> GetAllCourseAsync()
    {
        var response = await _httpClient.GetAsync("courses");

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync <Response<List<CourseViewModel>>>();
        
        result.Data.ForEach(x =>
        {
            x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
        });

        return result.Data;
    }

    public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
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
        
        result.Data.ForEach(x =>
        {
            x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
        });

        return result.Data;
    }

    public async Task<CourseViewModel> GetByCourseId(string courseId)
    {
        var response = await _httpClient.GetAsync($"courses/{courseId}");

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

        result.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(result.Data.Picture);

        return result.Data;
    }

    public async Task<bool> CreateCourseAsync(CourseCreateInput model)
    {
        var resultPhoto = await _photoStockService.UploadPhoto(model.PhotoFormFile);

        if (resultPhoto != null)
        {
            model.Picture = resultPhoto.Url;
        }
        
        var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses", model);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(CourseUpdateInput model)
    {
        if (model.PhotoFormFile != null)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(model.PhotoFormFile);

            if (resultPhoto != null)
            {
                await _photoStockService.DeletePhoto(model.Picture);
                model.Picture = resultPhoto.Url;
            }
        }
        
        var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", model);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCourseAsync(string courseId, string pictureUrl)
    {
        if (!string.IsNullOrEmpty(pictureUrl))
            await _photoStockService.DeletePhoto(pictureUrl);

        var response = await _httpClient.DeleteAsync($"courses/{courseId}");

        return response.IsSuccessStatusCode;
    }
}