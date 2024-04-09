using CLED.WareHouse.Models.Database.Accessories;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices.AccessoryServices;

public class AccessoryService : IService<Accessory>
{
    public async Task<Accessory> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Accessory>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task Insert(Accessory accessory)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Accessory accessory)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}