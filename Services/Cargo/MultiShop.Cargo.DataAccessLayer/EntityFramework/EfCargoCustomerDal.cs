using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Context;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework;

public class EfCargoCustomerDal : GenericRepository<CargoCustomer> , ICargoCustomerDal

{
    public EfCargoCustomerDal(CargoContext context) : base(context)
    {
    }
}