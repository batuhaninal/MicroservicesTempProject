﻿using System.Text.Json.Serialization;

namespace FreeCourse.Services.Basket.Dtos
{
    public class BasketDto
    {
        //[JsonIgnore]
        public string? UserId { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }

        public BasketDto()
        {
            BasketItems = new List<BasketItemDto>();
        }
        public decimal TotalPrice 
        { 
            get=> BasketItems.Sum(x => x.Price * x.Quantity);  
        }
    }
}
