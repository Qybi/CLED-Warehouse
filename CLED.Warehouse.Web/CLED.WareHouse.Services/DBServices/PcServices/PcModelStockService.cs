using CLED.WareHouse.Models.Database.PCs;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices.PcServices;

public class PcModelStockService : IService<PcModelStock>
{
    public async Task<PcModelStock> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PcModelStock>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task Insert(PcModelStock pcModelStock)
    {
        throw new NotImplementedException();
    }

    public async Task Update(PcModelStock pcModelStock)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}