using CLED.WareHouse.Models.Database.PCs;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices.PcServices;

public class PcModelStockService : IService<PcModelStock>
{
    public Task<PcModelStock> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PcModelStock>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(PcModelStock pcModelStock)
    {
        throw new NotImplementedException();
    }

    public Task Update(PcModelStock pcModelStock)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}