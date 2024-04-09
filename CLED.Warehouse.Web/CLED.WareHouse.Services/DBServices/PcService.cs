using CLED.WareHouse.Services.DBServices.Interfaces;
using CLED.WareHouse.Models.Database.PCs;

namespace CLED.WareHouse.Services.DBServices;

public class PcService : IService<Pc>
{
    public Task<Pc> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task<Pc>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(Pc pc)
    {
        throw new NotImplementedException();
    }

    public Task Update(Pc pc)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}