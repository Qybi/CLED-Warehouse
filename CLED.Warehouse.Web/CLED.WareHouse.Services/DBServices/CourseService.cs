using CLED.WareHouse.Models.Database;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices;

public class CourseService : IService<Course>
{
    public Task<Course> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task<Course>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(Course obj)
    {
        throw new NotImplementedException();
    }

    public Task Update(Course obj)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}