using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers;

public class AuthController : Controller
{
    private readonly IIdentityService _identityService;

    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SigninInput signinInput)
    {
        if (!ModelState.IsValid)
            return View();

        var response = await _identityService.SignInAsync(signinInput);

        if (!response.IsSuccessful)
        {
            foreach (var error in response.Errors)
            {
                ModelState.AddModelError(String.Empty, error);
            }
            
            return View();
        }
        
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await _identityService.RevokeRefreshTokenAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}