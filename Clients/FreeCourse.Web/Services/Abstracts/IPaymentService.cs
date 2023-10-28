using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Abstracts;

public interface IPaymentService
{
    Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
}