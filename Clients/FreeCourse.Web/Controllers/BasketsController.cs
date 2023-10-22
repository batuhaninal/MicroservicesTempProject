using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers;
[Authorize]
public class BasketsController : Controller
{
    private readonly ICatalogService _catalogService;

    private readonly IBasketService _basketService;
    // GET
    public BasketsController(ICatalogService catalogService, IBasketService basketService)
    {
        _catalogService = catalogService;
        _basketService = basketService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _basketService.GetAsync());
    }
    
    public async Task<IActionResult> AddBasketItem(string courseId)
    {
        var course = await _catalogService.GetByCourseId(courseId);

        var basketItem = new BasketItemViewModel()
        {
            CourseId = courseId,
            Price = course.Price,
            CourseName = course.Name,
            Quantity = 1
        };

        await _basketService.AddBasketItemAsync(basketItem);

        return RedirectToAction(nameof(Index), "Baskets");
    }

    public async Task<IActionResult> RemoveBasketItem(string courseId)
    {
        await _basketService.RemoveBasketItemAsync(courseId);
        return RedirectToAction(nameof(Index), "Baskets");
    }
}