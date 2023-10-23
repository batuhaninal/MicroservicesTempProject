using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;

namespace FreeCourse.Web.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;
    private readonly IDiscountService _discountService;

    public BasketService(HttpClient httpClient, IDiscountService discountService)
    {
        _httpClient = httpClient;
        _discountService = discountService;
    }

    public async Task<bool> SaveOrUpdateAsync(BasketViewModel basketViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("baskets", basketViewModel);

        return response.IsSuccessStatusCode;
    }

    public async Task<BasketViewModel> GetAsync()
    {
        var response = await _httpClient.GetAsync("baskets");
        if (!response.IsSuccessStatusCode)
            return null;

        var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();
        return basketViewModel.Data;
    }

    public async Task<bool> DeleteAsync()
    {
        var response = await _httpClient.DeleteAsync("baskets");
        
        return response.IsSuccessStatusCode;
    }

    public async Task AddBasketItemAsync(BasketItemViewModel basketItemViewModel)
    {
        var basket = await GetAsync();

        if (basket != null)
        {
            if (basket.BasketItems.All(x => x.CourseId != basketItemViewModel.CourseId))
            {
                basket.BasketItems.Add(basketItemViewModel);
            }
        }
        else
        {
            basket = new BasketViewModel();
            basket.BasketItems.Add(basketItemViewModel);
        }

        await SaveOrUpdateAsync(basket);
    }

    public async Task<bool> RemoveBasketItemAsync(string courseId)
    {
        var basket = await GetAsync();

        if (basket == null)
            return false;

        var deletedBasketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);

        if (deletedBasketItem == null)
            return false;

        var deleteResult = basket.BasketItems.Remove(deletedBasketItem);

        if (!deleteResult)
            return false;

        if (!basket.BasketItems.Any())
            basket.DiscountCode = null;

        return await SaveOrUpdateAsync(basket);
    }

    public async Task<bool> ApplyDiscountAsync(string discountCode)
    {
        await CancelApplyDiscountAsync();

        var basket = await GetAsync();

        if (basket == null)
            return false;
        
        var hasDiscount = await _discountService.GetDiscountAsync(discountCode);

        if (hasDiscount == null)
            return false;
        
        basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);

        return await SaveOrUpdateAsync(basket);
    }

    public async Task<bool> CancelApplyDiscountAsync()
    {
        var basket = await GetAsync();

        if (basket == null || basket.DiscountCode == null)
            return false;

        basket.CancelAppliedDiscount();
        
        return await SaveOrUpdateAsync(basket);
    }
}