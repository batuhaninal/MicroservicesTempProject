using FreeCourse.Shared.Services;
using FreeCourse.Web.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ISharedIdentityService _sharedIdentityService;
    // GET
    public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
    {
        _catalogService = catalogService;
        _sharedIdentityService = sharedIdentityService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
    }
}