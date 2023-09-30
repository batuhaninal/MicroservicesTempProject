using Dapper;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> AddAsync(Models.Discount discount)
        {
            //var status = await _connection.ExecuteAsync("insert into discount (userId,rate,code) values (@UserId,@Rate,@Code)", new {UserId=discount.UserId, Rate=discount.Rate, Code = discount.Code});
            var status = await _connection.ExecuteAsync("insert into discount (userId,rate,code) values (@UserId,@Rate,@Code)", discount);

            if(status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("An error accured while adding",500);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var status = await _connection.ExecuteAsync("delete from discount where id=@Id", new { id });

            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<List<Models.Discount>>> GetAllAsync()
        {
            var discount = await _connection.QueryAsync<Models.Discount>("select * from discount");

            return Response<List<Models.Discount>>.Success(discount.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeandUserId(string code, string userId)
        {
            var discount = (await _connection.QueryAsync<Models.Discount>("select * from discount where code=@Code and userId=@UserId",new { Code=code,UserId=userId})).SingleOrDefault();

            if (discount == null)
                return Response<Models.Discount>.Fail("Discount not found", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetByIdAsync(int id)
        {
            var discount = (await _connection.QueryAsync<Models.Discount>("select * from discount where id=@Id", new { Id=id })).SingleOrDefault();
            

            if(discount == null)
                return Response<Models.Discount>.Fail("Discount not found", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(Models.Discount discount)
        {
            //var status = await _connection.ExecuteAsync("update discount set userId=@UserId, code=@Code, rate=@Rate where id=@Id",discount);
            var status = await _connection.ExecuteAsync("update discount set userId=@UserId, code=@Code, rate=@Rate where id=@Id", new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });

            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}
