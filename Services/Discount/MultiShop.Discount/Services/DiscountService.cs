using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public class DiscountService : IDiscountService
{
    private readonly DapperContext _context;

    public DiscountService(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
    {
        string query = "Select * from Coupons";
        using (var connection = _context.CreateConnection())
        {
            var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
            return values.ToList();
        }
        
    }

    public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createDiscountCouponDto)
    {
        string query = "insert into Coupons (Code,Rate,IsActive,ValidDate) values(@Code,@Rate,@IsActive,@ValidDate)";
        var parameters = new DynamicParameters();
        parameters.Add("Code", createDiscountCouponDto.Code);
        parameters.Add("Rate", createDiscountCouponDto.Rate);
        parameters.Add("IsActive", createDiscountCouponDto.IsActive);
        parameters.Add("ValidDate", createDiscountCouponDto.ValidDate);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateDiscountCouponDto)
    {
        string query = @"UPDATE Coupons
                 SET Code=@Code, Rate=@Rate, IsActive=@IsActive, ValidDate=@ValidDate
                 WHERE CouponId=@CouponId";
        var parameters = new DynamicParameters();
        parameters.Add("Code", updateDiscountCouponDto.Code);
        parameters.Add("Rate", updateDiscountCouponDto.Rate);
        parameters.Add("IsActive", updateDiscountCouponDto.IsActive);
        parameters.Add("ValidDate", updateDiscountCouponDto.ValidDate);
        parameters.Add("CouponId", updateDiscountCouponDto.CouponId);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
        

    }

    public async Task DeleteDiscountCouponAsync(int id)
    {
        string query = "Delete From Coupons Where CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("couponId", id);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task<GetByIdDiscountCouponDto?> GetByIdDiscountCouponAsync(int id)
    {
        string query = "SELECT * FROM Coupons WHERE CouponId = @CouponId";
        var parameters = new DynamicParameters();
        parameters.Add("CouponId", id);

        using (var connection = _context.CreateConnection())
        {
            var value = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query, parameters);
            return value;
        }
    }

}