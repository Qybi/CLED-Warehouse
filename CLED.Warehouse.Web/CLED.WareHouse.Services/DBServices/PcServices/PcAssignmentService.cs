using CLED.WareHouse.Models.Database.PCs;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices;

public class PcAssignmentService : IService<PcAssignment>
{
    public Task<PcAssignment> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task<PcAssignment>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(PcAssignment pcAssignment)
    {
        throw new NotImplementedException();
    }

    public Task Update(PcAssignment pcAssignment)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}