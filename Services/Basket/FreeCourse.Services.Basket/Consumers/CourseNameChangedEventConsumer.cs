using System.Text.Json;
using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.Messages;
using FreeCourse.Shared.Services;
using MassTransit;

namespace FreeCourse.Services.Basket.Consumers;

public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
{
    private readonly RedisService _redisService;

    public CourseNameChangedEventConsumer(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
    {
        var userId = context.Message.UserId;
        var basket = await _redisService.GetDb().StringGetAsync(userId);
        var basketDto = JsonSerializer.Deserialize<BasketDto>(basket);

        basketDto.BasketItems
            .FindAll(x => x.CourseId == context.Message.CourseId)
            .ForEach(x => { x.CourseName = context.Message.UpdatedName; });

        await _redisService.GetDb().StringSetAsync(userId, JsonSerializer.Serialize(basketDto));
    }
}