using CLED.WareHouse.Models.Database.Reasons;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices.ReasonsServices;

public class ReasonReturnService : IService<ReasonsReturn>
{
    public Task<ReasonsReturn> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReasonsReturn>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(ReasonsReturn reasonsReturn)
    {
        throw new NotImplementedException();
    }

    public Task Update(ReasonsReturn reasonsReturn)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}