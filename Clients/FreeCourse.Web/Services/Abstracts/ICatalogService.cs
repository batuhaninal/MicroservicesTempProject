using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Abstracts;

public interface ICatalogService
{
    Task<List<CourseViewModel>> GetAllCourseAsync();
    Task<List<CategoryViewModel>> GetAllCategoriesAsync();
    Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
    Task<CourseViewModel> GetByCourseId(string courseId);
    Task<bool> CreateCourseAsync(CourseCreateInput model);
    Task<bool> UpdateCourseAsync(CourseUpdateInput model);
    Task<bool> DeleteCourseAsync(string courseId, string pictureUrl);
}