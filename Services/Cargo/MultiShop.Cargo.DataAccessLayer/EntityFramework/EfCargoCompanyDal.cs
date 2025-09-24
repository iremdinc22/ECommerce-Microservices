using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Context;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework;

public class EfCargoCompanyDal : GenericRepository<CargoCompany> , ICargoCompanyDal
{
    public EfCargoCompanyDal(CargoContext context) : base(context)
    {
    }
}