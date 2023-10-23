using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Abstracts;

public interface IDiscountService
{
    Task<DiscountViewModel> GetDiscountAsync(string discountCode);
}