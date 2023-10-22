using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;

namespace FreeCourse.Web.Services;

public class PhotoStockService : IPhotoStockService
{
    private readonly HttpClient _httpClient;

    public PhotoStockService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PhotoViewModel> UploadPhotoAsync(IFormFile photo)
    {
        if (photo == null || photo.Length <= 0)
            return null;

        var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

        using var ms = new MemoryStream();
        await photo.CopyToAsync(ms);

        var multipartContent = new MultipartFormDataContent();
        
        multipartContent.Add(new ByteArrayContent(ms.ToArray()),"photo", randomFileName);

        var response = await _httpClient.PostAsync("photos", multipartContent);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();
        return result.Data;
    }

    public async Task<bool> DeletePhotoAsync(string photoUrl)
    {
        var response = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");

        return response.IsSuccessStatusCode;
    }
}