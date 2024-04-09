using CLED.WareHouse.Models.Database.Accessories;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices.AccessoryServices;

public class AccessoryAssignmentService : IService<AccessoryAssignment>
{
    public Task<AccessoryAssignment> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AccessoryAssignment>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(AccessoryAssignment accessoryAssignment)
    {
        throw new NotImplementedException();
    }

    public Task Update(AccessoryAssignment accessoryAssignment)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}