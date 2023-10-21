using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Abstracts;

public interface IPhotoStockService
{
    Task<PhotoViewModel> UploadPhoto(IFormFile photo);
    Task<bool> DeletePhoto(string photoUrl);
}