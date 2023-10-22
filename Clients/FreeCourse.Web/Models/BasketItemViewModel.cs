namespace FreeCourse.Web.Models;

public class BasketItemViewModel
{
    public int Quantity { get; set; } = 1;
    public string CourseId { get; set; }
    public string CourseName { get; set; }
    public decimal Price { get; set; }
    private decimal? DiscountAppliedPrice { get; set; }
    
    public decimal GetCurrentPrice => DiscountAppliedPrice ?? Price;

    public void AppliedDiscount(decimal discountPrice)
    {
        DiscountAppliedPrice = discountPrice;
    }
}