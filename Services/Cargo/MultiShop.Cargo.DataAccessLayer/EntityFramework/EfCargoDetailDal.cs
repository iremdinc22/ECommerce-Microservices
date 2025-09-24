using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Context;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework;

public class EfCargoDetailDal : GenericRepository<CargoDetail> , ICargoDetailDal
{
    public EfCargoDetailDal(CargoContext context) : base(context)
    {
    }
}