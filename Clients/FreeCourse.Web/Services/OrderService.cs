using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;

namespace FreeCourse.Web.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly IPaymentService _paymentService;
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
    {
        _httpClient = httpClient;
        _paymentService = paymentService;
        _basketService = basketService;
        _sharedIdentityService = sharedIdentityService;
    }

    public async Task<OrderCreatedViewModel> CreateOrderAsync(CheckoutInfoInput checkoutInfoInput)
    {
        var basket = await _basketService.GetAsync();

        var payment = new PaymentInfoInput()
        {
            CardName = checkoutInfoInput.CardName,
            CardNumber = checkoutInfoInput.CardNumber,
            CVV = checkoutInfoInput.CVV,
            Expiration = checkoutInfoInput.Expiration,
            TotalPrice = basket.TotalPrice
        };

        var responsePayment = await _paymentService.ReceivePayment(payment);

        if (!responsePayment)
            return new OrderCreatedViewModel() { Error = "Ödeme alınamadı!", IsSuccessfull = false };

        var orderCreateInput = new OrderCreateInput()
        {
            BuyerId = _sharedIdentityService.GetUserId,
            Address = new AddressCreateInput()
            {
                Province = checkoutInfoInput.Province,
                District = checkoutInfoInput.District,
                Street = checkoutInfoInput.Street,
                Line = checkoutInfoInput.Line,
                ZipCode = checkoutInfoInput.ZipCode
            }
        };
        
        basket.BasketItems.ForEach(x =>
        {
            var orderItem = new OrderItemCreateInput()
            {
                ProductId = x.CourseId,
                Price = x.GetCurrentPrice,
                PictureUrl = "",
                ProductName = x.CourseName
            };
            orderCreateInput.OrderItems.Add(orderItem);
        });

        var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);
        
        if(!response.IsSuccessStatusCode)
            return new OrderCreatedViewModel() { Error = "Sipariş oluşturulamadı!", IsSuccessfull = false };
        
        var result = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
        result.Data.IsSuccessfull = true;

        await _basketService.DeleteAsync();
        return result.Data;
    }

    public async Task<OrderSuspendViewModel> SuspendOrderAsync(CheckoutInfoInput checkoutInfoInput)
    {
        var basket = await _basketService.GetAsync();
        
        var orderCreateInput = new OrderCreateInput()
        {
            BuyerId = _sharedIdentityService.GetUserId,
            Address = new AddressCreateInput()
            {
                Province = checkoutInfoInput.Province,
                District = checkoutInfoInput.District,
                Street = checkoutInfoInput.Street,
                Line = checkoutInfoInput.Line,
                ZipCode = checkoutInfoInput.ZipCode
            }
        };
        
        basket.BasketItems.ForEach(x =>
        {
            var orderItem = new OrderItemCreateInput()
            {
                ProductId = x.CourseId,
                Price = x.GetCurrentPrice,
                PictureUrl = "",
                ProductName = x.CourseName
            };
            orderCreateInput.OrderItems.Add(orderItem);
        });
        
        var payment = new PaymentInfoInput()
        {
            CardName = checkoutInfoInput.CardName,
            CardNumber = checkoutInfoInput.CardNumber,
            CVV = checkoutInfoInput.CVV,
            Expiration = checkoutInfoInput.Expiration,
            TotalPrice = basket.TotalPrice,
            Order = orderCreateInput
        };
        
        var responsePayment = await _paymentService.ReceivePayment(payment);

        if (!responsePayment)
            return new OrderSuspendViewModel() { Error = "Ödeme alınamadı!", IsSuccessfull = false };

        await _basketService.DeleteAsync();
        
        return new OrderSuspendViewModel() { IsSuccessfull = true };
    }

    public async Task<List<OrderViewModel>> GetOrdersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

        return response.Data;
    }
}