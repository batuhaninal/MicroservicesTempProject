﻿namespace FreeCourse.Web.Models;

public class BasketViewModel
{
    public string? UserId { get; set; }
    public string? DiscountCode { get; set; }
    public int? DiscountRate { get; set; }
    private List<BasketItemViewModel> _basketItems { get; set; }

    public List<BasketItemViewModel> BasketItems
    {
        get
        {
            if (HasDiscount)
            {
                _basketItems.ForEach(x =>
                {
                    var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                    x.AppliedDiscount(Math.Round(x.Price - discountPrice,2));
                });
            }
            return _basketItems;
        }
        set => _basketItems = value;
    }
    
    public decimal TotalPrice => _basketItems.Sum(x => x.GetCurrentPrice * x.Quantity);

    public bool HasDiscount => !string.IsNullOrEmpty(DiscountCode) && DiscountRate.HasValue;

    public BasketViewModel()
    {
        _basketItems = new List<BasketItemViewModel>();
    }

    public void CancelAppliedDiscount()
    {
        DiscountCode = null;
        DiscountRate = null;
    }

    public void ApplyDiscount(string code, int rate)
    {
        DiscountCode = code;
        DiscountRate = rate;
    }
}