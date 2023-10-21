using FreeCourse.Shared.Services;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _catalogService.GetAllCategoriesAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateInput model)
    {
        var categories = await _catalogService.GetAllCategoriesAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        model.UserId = _sharedIdentityService.GetUserId;
        if (!ModelState.IsValid)
            return View();


        await _catalogService.CreateCourseAsync(model);
        return RedirectToAction(nameof(Index), "Courses");
    }

    [HttpGet]
    public async Task<IActionResult> Update(string id)
    {
        var course = await _catalogService.GetByCourseId(id);
        var categories = await _catalogService.GetAllCategoriesAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");

        if (course == null)
            return RedirectToAction(nameof(Index), "Courses");

        var courseUpdateInput = new CourseUpdateInput()
        {
            Id = course.Id,
            Name = course.Name,
            Price = course.Price,
            Description = course.Description,
            CategoryId = course.CategoryId,
            Feature = course.Feature,
            Picture = course.Picture,
            UserId = course.UserId
        };

        return View(courseUpdateInput);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CourseUpdateInput model)
    {
        var categories = await _catalogService.GetAllCategoriesAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        if (!ModelState.IsValid)
            return View();

        await _catalogService.UpdateCourseAsync(model);
        
        return RedirectToAction(nameof(Index), "Courses");
    }

    public async Task<IActionResult> Delete(string id)
    {
        await _catalogService.DeleteCourseAsync(id);

        return RedirectToAction(nameof(Index), "Courses");
    }
}