using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResulInstance(await _discountService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return CreateActionResulInstance(await _discountService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Models.Discount discount)
        {
            return CreateActionResulInstance(await _discountService.AddAsync(discount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return CreateActionResulInstance(await _discountService.DeleteAsync(id));
        }

        
        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            return CreateActionResulInstance(await _discountService.GetByCodeandUserId(code, _sharedIdentityService.GetUserId));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Models.Discount discount)
        {
            return CreateActionResulInstance(await _discountService.UpdateAsync(discount));
        }
    }
}
