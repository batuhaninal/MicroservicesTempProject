using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Abstracts;

public interface IPhotoStockService
{
    Task<PhotoViewModel> UploadPhotoAsync(IFormFile photo);
    Task<bool> DeletePhotoAsync(string photoUrl);
}