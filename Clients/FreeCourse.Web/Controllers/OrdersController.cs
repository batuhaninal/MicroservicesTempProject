using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IBasketService _basketService;

    private readonly IOrderService _orderService;
    // GET
    public OrdersController(IBasketService basketService, IOrderService orderService)
    {
        _basketService = basketService;
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var basket = await _basketService.GetAsync();
        ViewBag.basket = basket;
        return View(new CheckoutInfoInput());
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
    {
        var basket = await _basketService.GetAsync();
        if (!ModelState.IsValid)
        {
            ViewBag.basket = basket;
            return View();
        }
        // var orderStatus = await _orderService.CreateOrderAsync(checkoutInfoInput);

        var orderSuspend = await _orderService.SuspendOrderAsync(checkoutInfoInput);
        
        if (!orderSuspend.IsSuccessfull)
        {
            ViewBag.error = orderSuspend.Error;
            ViewBag.basket = basket;
            return View();
        }

        // await _basketService.DeleteAsync();

        // return RedirectToAction(nameof(SuccessfullCheckout), new { orderId = orderStatus.OrderId });
        
        return RedirectToAction(nameof(SuccessfullCheckout), new { orderId = new Random().Next(1,1000) });

    }

    public IActionResult SuccessfullCheckout(int orderId)
    {
        ViewBag.orderId = orderId;
        return View();
    }

    public async Task<IActionResult> CheckoutHistory()
    {
        return View(await _orderService.GetOrdersAsync());
    }
}