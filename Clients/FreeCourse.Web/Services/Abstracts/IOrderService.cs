using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Abstracts;

public interface IOrderService
{
    // Senkron iletisim
    Task<OrderCreatedViewModel> CreateOrderAsync(CheckoutInfoInput checkoutInfoInput);
    // Asenkron iletisim => Siparis bilgileri rabbitmq'ya gonderilecek
    Task<OrderSuspendViewModel> SuspendOrderAsync(CheckoutInfoInput checkoutInfoInput);
    Task<List<OrderViewModel>> GetOrdersAsync();
}