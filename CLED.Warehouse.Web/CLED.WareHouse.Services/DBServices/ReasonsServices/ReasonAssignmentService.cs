using CLED.WareHouse.Models.Database.Reasons;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices.ReasonsServices;

public class ReasonAssignmentService : IService<ReasonsAssignment>
{
    public Task<ReasonsAssignment> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReasonsAssignment>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(ReasonsAssignment reasonsAssignment)
    {
        throw new NotImplementedException();
    }

    public Task Update(ReasonsAssignment reasonsAssignment)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}