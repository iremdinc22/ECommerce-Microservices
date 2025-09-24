using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.BusinessLayer.Concrete;

public class CargoCustomerManager : ICargoCustomerService   
{
    private readonly ICargoCustomerDal _cargoCustomerDal;

    public CargoCustomerManager(ICargoCustomerDal cargoCustomerDal)
    {
        _cargoCustomerDal = cargoCustomerDal;
    }

    public void TInsert(CargoCustomer entity)
    {
        _cargoCustomerDal.Insert(entity);
    }

    public void TUpdate(CargoCustomer entity)
    {
       _cargoCustomerDal.Update(entity);
    }

    public void TDelete(int id)
    {
       _cargoCustomerDal.Delete(id);
    }

    public CargoCustomer TGetById(int id)
    {
       return _cargoCustomerDal.GetById(id);
    }

    public List<CargoCustomer> TGetAll()
    {
       return _cargoCustomerDal.GetAll();
    }
}