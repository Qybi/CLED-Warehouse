using CLED.WareHouse.Models.Database.Accessories;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices;

public class AccessoryService : IService<Accessory>
{
    public Task<Accessory> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task<Accessory>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(Accessory accessory)
    {
        throw new NotImplementedException();
    }

    public Task Update(Accessory accessory)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}