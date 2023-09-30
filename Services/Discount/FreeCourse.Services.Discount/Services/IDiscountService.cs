using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Models.Discount>>> GetAllAsync();
        Task<Response<Models.Discount>> GetByIdAsync(int id);
        Task<Response<NoContent>> AddAsync(Models.Discount discount);
        Task<Response<NoContent>> UpdateAsync(Models.Discount discount);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<Models.Discount>> GetByCodeandUserId(string code, string userId);
    }
}
