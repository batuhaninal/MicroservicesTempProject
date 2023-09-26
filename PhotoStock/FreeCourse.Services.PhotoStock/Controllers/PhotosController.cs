using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave([FromForm] IFormFile photo, CancellationToken cancellationToken)
        {
            if(photo is not null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);
                }

                var returnPath = $"photos/{photo.FileName}";

                return CreateActionResulInstance(Response<PhotoDto>.Success(new PhotoDto { Url = returnPath }, 200));
            }

            return CreateActionResulInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
        }

        [HttpDelete]
        public IActionResult PhotoDelete([FromQuery] string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
                return CreateActionResulInstance(Response<NoContent>.Fail("Photo not found", 404));

            System.IO.File.Delete(path);

            return CreateActionResulInstance(Response<NoContent>.Success(204));
        }
    }
}
