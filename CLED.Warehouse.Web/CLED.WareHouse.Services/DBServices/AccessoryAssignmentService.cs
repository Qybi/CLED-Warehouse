using CLED.WareHouse.Models.Database.Accessories;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices;

public class AccessoryAssignmentService : IService<AccessoryAssignment>
{
    public Task<AccessoryAssignment> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task<AccessoryAssignment>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(AccessoryAssignment obj)
    {
        throw new NotImplementedException();
    }

    public Task Update(AccessoryAssignment obj)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}